using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Control { 
    Forward,
    Backward,
    Left,
    Right,
    Interact,
    //add macguffins here


    None
}

public class Movement : MonoBehaviour
{
    //control mapping
    Dictionary<KeyCode, Control> controls;

    //movement physics
    Rigidbody body;
    float speed = 1.0f;
    Vector3 velocity = new Vector3(0,0,0);
    bool attached = false;
    Transform direction;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        //init control map then fill them with keys

    }

    void Update()
    {
        //apply physics
        if (velocity != Vector3.zero)
        {
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
            //gravity and drag embedded in rigidbody
        }

    }

    public void Attach(Transform dir) {
        attached = true;
        direction = dir;
    }
    public void Detatch() {
        attached = false;
        direction = null;
    }



    // call this function each FixedUpdate to determine movement
    public void Move(KeyCode[] keys) {

        float forward = 0;
        float right = 0;

        foreach(KeyCode key in keys) {
            if (controls.ContainsKey(key))
            {
                //check for movement
                if (controls[key] == Control.Forward)
                {
                    forward += 1;
                }
                if (controls[key] == Control.Backward)
                {
                    forward -= 1;
                }
                if (controls[key] == Control.Left)
                {
                    right -= 1;
                }
                if (controls[key] == Control.Right)
                {
                    right += 1;
                }
            }
            else
            {
                Debug.Log("Key is not set");
            }
        }

        //update physics
        Vector3 moveHorizontal = transform.right * right;
        Vector3 moveVertical = transform.forward * forward;
        velocity = (moveHorizontal + moveVertical).normalized * speed;


    }
}
