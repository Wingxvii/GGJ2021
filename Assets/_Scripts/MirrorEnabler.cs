using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEnabler : MonoBehaviour
{
    public GameObject reflection;
    public GameObject mirror;

    private void Start()
    {
        reflection.SetActive(false);
        mirror.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera") {
            Debug.Log("Entered into mirror");
            reflection.SetActive(true);
            mirror.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("Exited Mirror");
            reflection.SetActive(false);
            mirror.SetActive(false);
        }
    }
}
