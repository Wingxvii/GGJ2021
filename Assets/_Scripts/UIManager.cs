using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //singleton stuff
    #region SingletonCode
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
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

    public GameObject cursor;
    public GameObject[] beers;
    public GameObject textBox;
    public Text textBoxText;
    public Animator textBoxAnim;

    public GameObject[] keysStack;

    private void Start()
    {
        cursor.SetActive(false);
        RemoveBeers();
        RemoveKeys();
    }

    public void UpdateBeer(int tolerance) 
    {
        RemoveBeers();

        if (tolerance > 0) 
        {
            beers[0].SetActive(true);
        }
        if (tolerance > 1)
        {
            beers[1].SetActive(true);
        }
        if (tolerance > 2)
        {
            beers[2].SetActive(true);
        }
        if (tolerance > 3)
        {
            beers[3].SetActive(true);
        }
        if (tolerance > 4)
        {
            beers[4].SetActive(true);
        }
    }

    public void UpdateKeys(int[] keys)
    {
        RemoveKeys();
        foreach (int key in keys) {
            keysStack[key-1].SetActive(true);
        }
    }

    //call this when dispossessing
    public void ResetBody() {
        RemoveBeers();
        RemoveKeys();
    }

    void RemoveBeers() {
        foreach (GameObject beer in beers) {
            beer.SetActive(false);
        }
    }

    void RemoveKeys() {
        foreach (GameObject key in keysStack)
        {
            key.SetActive(false);
        }

    }

    //call this for user notifications
    public void PlayText(string text) {
        textBoxText.text = text;
        textBoxAnim.Play("fadeout", -1, 0f);
    }
}
