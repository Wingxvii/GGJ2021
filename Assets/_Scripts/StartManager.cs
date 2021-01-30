using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    //singleton stuff
    #region SingletonCode
    private static StartManager _instance;
    public static StartManager Instance { get { return _instance; } }
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

    public GameObject GameStartObject;
    public GameObject GameMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        GameStartObject.SetActive(false);
        GameMenuObject.SetActive(true);
    }

    public void OnMenuButton() {
        GameStartObject.SetActive(true);
        GameMenuObject.SetActive(false);
    }

    public void OnGameStartButton() {
        SceneManager.LoadScene(1);
    }

}
