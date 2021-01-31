using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlTools;

public class GhostController : MonoBehaviour
{
    //singleton stuff
    #region SingletonCode
    private static GhostController _instance;
    public static GhostController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    //single pattern ends here
    #endregion


    //physics
    float xRotation = 0.0f;
    float yRotation = 0.0f;
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;
    public float ghostSpeed = 1.0f;

    //posession
    public bool attached = false;
    Body body;
    GameObject interactHitObject;
    bool interactHit = false;

    //interaction
    float interactionDist = 5.0f;
    public LayerMask interactableMask;

    //postProcessing
    public Postprocessing postprocessing;

    // Start is called before the first frame update
    void Start()
    {
        attached = false;
        Cursor.visible = false;

        //add interactable items
        interactableMask = LayerMask.GetMask("Body");
        interactableMask += LayerMask.GetMask("Door");
        interactableMask += LayerMask.GetMask("Beer");

        //reset BeerBlur if it's in effect
        ResetBeerBlur();

    }

    // Update is called once per frame
    void Update()
    {
        //rotation movement
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        this.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

        if (attached)
        {
            //control body instead of ghost
            foreach (KeyCode key in ControlScheme.viableKeys)
            {
                if (Input.GetKey(key))
                {
                    body.Move(key);
                }

                //handle interact key press
                if (interactHit && Input.GetKeyDown(key) && body.Interact(key)) {
                    switch (interactHitObject.tag)
                    {
                        case "Body":
                            Debug.Log("Cannot Possess when not in ghost form");
                            UIManager.Instance.PlayText("Cannot Possess when not in ghost form");
                            break;
                        case "Door":
                            //check key and open door
                            if (body.keys.Contains(interactHitObject.GetComponent<DoorAOpen>().keyHole))
                            {
                                interactHitObject.GetComponent<DoorAOpen>().Open();
                                Debug.Log("You have opened a Door.");
                                UIManager.Instance.PlayText("You have opened a Door.");
                            }
                            else {
                                Debug.Log("You do not have the required key to open this door.");
                                UIManager.Instance.PlayText("You do not have the required key to open this door.");
                            }
                            break;
                        case "Beer":
                            if (interactHitObject.GetComponent<Beer>().Chug()) 
                            {
                                Debug.Log("Drinking Beer");
                                UIManager.Instance.PlayText("Drinking Beer");
                                bool passed_out = body.Drink();
                                SetBeerBlur();
                                if (passed_out) {
                                    Debug.Log("You passed out");
                                    UIManager.Instance.PlayText("You passed out");
                                    DisPossess();
                                }
                            }

                            break;
                        default:
                            Debug.LogWarning("Not Acceptable Interaction");
                            break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))       //TEMP CODE FOR DISPOSSESSING
            {
                if (attached)
                {
                    DisPossess();
                }
            }
        }
        else
        {
            float vertical = 0;
            float horizontal = 0;

            //get inputs
            if (Input.GetKey(KeyCode.W))
            {
                vertical += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                horizontal -= 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vertical -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontal += 1;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (interactHit)
                {
                    switch (interactHitObject.tag)
                    {
                        case "Body":
                            // Attempt Posession
                            Possess(interactHitObject);
                            break;
                        case "Door":
                            UIManager.Instance.PlayText("Cannot Open Doors without Body");
                            Debug.Log("Cannot Open Doors without Body");
                            break;
                        case "Beer":
                            UIManager.Instance.PlayText("Cannot Drink without Body");
                            Debug.Log("Cannot Drink without Body");
                            break;
                        default:
                            Debug.LogWarning("Not Acceptable Interaction");
                            break;
                    }
                }
            }
            //movement
            Vector3 dir = new Vector3(horizontal, 0, vertical);
            dir = transform.TransformDirection(dir);

            dir *= ghostSpeed;

            transform.position += dir * Time.deltaTime;

            //clamping
            if (transform.position.y <= 1)
            {
                transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
            }

        }
    }

    void ResetBeerBlur() {
        postprocessing.ChangeBlurAmount(0);
    }

    void SetBeerBlur()
    {
        //get beer percentage amount from body
        float blur_percentage = (float) (body.max_tolerance - body.tolerance) / body.max_tolerance;

        Debug.Log(blur_percentage);

        postprocessing.ChangeBlurAmount(blur_percentage);
    }

    private void FixedUpdate()
    {
        //update sensor
        InteractionSensor();
    }

    //updates interaction raycast
    void InteractionSensor()
    {
        Ray raycast;

        raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, interactionDist, interactableMask))
        {
            interactHitObject = hit.transform.gameObject;
            interactHit = true;
            Outline outline;

            //turn on outline
            if (interactHitObject.TryGetComponent<Outline>(out outline)) {
                outline.OutlineWidth = 6;
                outline.keepOn = true;
            }

            //update interaction UI here
            UIManager.Instance.cursor.SetActive(true);
        }
        else
        {
            interactHit = false;
            UIManager.Instance.cursor.SetActive(false);
        }
        //draw sensors
        Debug.DrawLine(raycast.origin, raycast.origin + (transform.forward * interactionDist), Color.red);
    }

    public void Possess(GameObject target)
    {
        body = target.GetComponent<Body>();

        if (!body.blackedOut)
        {
            Debug.Log("Possessing");
            this.transform.parent = target.transform;
            attached = true;
            body.Attach(this.transform);
            StartCoroutine(LerpTo(1.0f, body.head));
            SetBeerBlur();
        }
        else {
            Debug.Log("Cannot possess blacked out");
        }
    }
    public void DisPossess() {
        Debug.Log("DisPossessing");
        this.transform.parent = null;
        attached = false;
        body.Detatch();
        ResetBeerBlur();
    }

    IEnumerator LerpTo(float time, Transform target)
    {
        float elapsedTime = 0;
        Vector3 currentPos = transform.position;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(currentPos, target.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        transform.position = target.position;
        yield return null;
    }

}