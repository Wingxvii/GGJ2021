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
    GameObject interactHit;

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

        if (attached) {
            //control body instead of ghost
            foreach (KeyCode key in ControlScheme.viableKeys) {
                if (Input.GetKey(key)) {
                    body.Move(key);
                }
            }
        }
        else{
            float vertical = 0;
            float horizontal = 0;

            if (Input.GetKey(KeyCode.W)) {
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

            //movement
            Vector3 dir = new Vector3(horizontal, 0, vertical);
            dir = transform.TransformDirection(dir);

            dir *= ghostSpeed;

            transform.position += dir * Time.deltaTime;

            //clamping
            if (transform.position.y <= 1) {
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
    void InteractionSensor() {
        Ray raycast;

        raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, interactionDist, interactableMask))
        {
            interactHit = hit.transform.gameObject;

            if (interactHit.tag == "Body") {
                Debug.Log("Body Hit");
            }
        }
        //draw sensors
        Debug.DrawLine(raycast.origin, raycast.origin + (transform.forward * interactionDist), Color.red);


    }
}
