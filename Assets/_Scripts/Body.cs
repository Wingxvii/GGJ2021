using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlTools;

namespace ControlTools
{
    public enum Control
    {
        Forward,
        Backward,
        Left,
        Right,
        Interact,
        //add macguffins here

        None
    }
}

public class Body : MonoBehaviour
{
    //control mapping
    Dictionary<KeyCode, Control> controls;

    //movement physics
    Rigidbody body;
    public float speed = 1.0f;
    public float gravity = 1.0f;
    Vector3 velocity = new Vector3(0,0,0);
    public bool attached = false;
    public Transform direction;
    float forward = 0;
    float right = 0;

    //credentials
    public List<int> keys = new List<int>();  //use editor to add key for each body

    //beer
    public bool blackedOut = false;
    public int max_tolerance = 3;
    public int tolerance = 3; // this gets overwritten to max_tolerance at game start.
    // in the event that we want different max_tolerances for each person.


    //animator
    public Animator bodyAnim;
    public Transform head;


    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        direction = this.transform;
        bodyAnim = this.GetComponent<Animator>();
        max_tolerance = tolerance;

        AssignRandomControls();
        /*
        foreach (KeyValuePair<KeyCode, Control> control in controls)
        {
            Debug.Log(control.Key);
            Debug.Log(control.Value);
        }
        */
    }

    void AssignRandomControls()
    {
        //init control map then randomly fill them with keys
        controls = new Dictionary<KeyCode, Control>();

        foreach (KeyCode key in ControlScheme.viableKeys) {
            float assignment = Random.Range(0.0f, 10.0f);
            //Debug.Log(assignment);
            //TODO: Maybe ensure each control type is included
            // philly: we could iterate through each of the control
            // before the foreach loop (i.e. assign direction keys + use)
            // ensuring that those controls are at least in one key.
            // then we continue with this random assignment
            // but skip any keys that have already been assigned.
            if (assignment <= 1.0f) {
                controls.Add(key, Control.Forward);
            }
            else if (assignment <= 2.0f) {
                controls.Add(key, Control.Backward);
            }
            else if (assignment <= 3.0f) {
                controls.Add(key, Control.Left);
            }
            else if (assignment <= 4.0f) {
                controls.Add(key, Control.Right);
            }
            else if (assignment <= 5.0f) {
                controls.Add(key, Control.Interact);
            }
            else {
                controls.Add(key, Control.None);
            }
        }
    }

    void FixedUpdate()
    {
        //update physics
        Vector3 moveHorizontal = new Vector3(direction.right.x * right, 0, direction.right.z * right);
        Vector3 moveVertical = new Vector3(direction.forward.x * forward, 0, direction.forward.z * forward);
        velocity = (moveHorizontal + moveVertical).normalized * speed;
        velocity -= new Vector3(0.0f, gravity, 0.0f);

        //apply physics
        if (velocity != Vector3.zero)
        {
            body.MovePosition(body.position + velocity);
        }

        //reset physics 
        forward = 0;
        right = 0;

    }

    private void LateUpdate()
    {
        //update model
        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, direction.forward.y, 0.0f));
    }
    //attach player ghost
    public void Attach(Transform dir) {
        attached = true;
        direction = dir;
        UIManager.Instance.UpdateBeer(tolerance);
        UIManager.Instance.UpdateKeys(keys.ToArray());
    }
    //detatch player ghost
    public void Detatch() {
        attached = false;
        direction = this.transform;
        UIManager.Instance.ResetBody();
    }

    //setup default controls
    public void DefaultControls() {
        controls[KeyCode.W] = Control.Forward;
        controls[KeyCode.A] = Control.Left;
        controls[KeyCode.S] = Control.Backward;
        controls[KeyCode.D] = Control.Right;
        controls[KeyCode.Mouse0] = Control.Interact;
    }


    // call this function each FixedUpdate to determine movement
    public void Move(KeyCode key) {
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

    // decrements tolerance, and returns True if we pass out.
    public bool Drink() {
        tolerance -= 1;
        UIManager.Instance.UpdateBeer(tolerance);
        if (tolerance == 0) {
            blackedOut = true;
            bodyAnim.SetTrigger("Blackout");
            return true;
        }
        return false;
    }

    //call this function for checking interaction
    public bool Interact(KeyCode key) { 
        if(controls[key] == Control.Interact)
        {
            return true;
        }
        return false;
    }
}
