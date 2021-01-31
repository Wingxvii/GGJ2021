using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    Body body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Body>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.attached) {
            SceneManager.LoadScene(2);
        }

    }
}
