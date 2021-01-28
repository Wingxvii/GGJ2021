using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        attached = false;
        Cursor.visible = false;
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

        //fetch input
        KeyCode[] keysDown;

        if (attached) { 
        
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
        }
    }
}
