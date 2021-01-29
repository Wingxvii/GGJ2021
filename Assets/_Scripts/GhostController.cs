using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlTools;

public class GhostController : MonoBehaviour
{
    //physics
    float xRotation = 0.0f;
    float yRotation = 0.0f;
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;
    public float ghostSpeed = 1.0f;

    //posession
    bool attached = false;
    Movement body;
    GameObject interactHitObject;
    bool interactHit = false;

    //interaction
    float interactionDist = 5.0f;
    public LayerMask interactableMask;

    // Start is called before the first frame update
    void Start()
    {
        attached = false;
        Cursor.visible = false;

        //add interactable items
        interactableMask = LayerMask.GetMask("Body");

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
        }
        else
        {
            interactHit = false;
        }
        //draw sensors
        Debug.DrawLine(raycast.origin, raycast.origin + (transform.forward * interactionDist), Color.red);
    }

    public void Possess(GameObject target)
    {
        Debug.Log("Possessing");
        this.transform.parent = target.transform;
        attached = true;
        body = target.GetComponent<Movement>();
        body.Attach(this.transform);
        StartCoroutine(LerpTo(1.0f, body.head));
    }
    public void DisPossess() {
        Debug.Log("DisPossessing");
        this.transform.parent = null;
        attached = false;
        body.Detatch();
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