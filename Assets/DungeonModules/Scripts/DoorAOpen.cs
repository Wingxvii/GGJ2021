using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    public int keyHole; //Use editor to change keyhole number
    public void Open()
    {
        GetComponentInParent<Animator>().SetTrigger("DoorATrigger");
    }
}