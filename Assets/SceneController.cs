using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            // Singletone pattern
            if (instance == null)
            {
                instance = new SceneController();
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GoPlay()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
