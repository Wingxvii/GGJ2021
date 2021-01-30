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

    private void Start()
    {
        cursor.SetActive(false);
    }
}
