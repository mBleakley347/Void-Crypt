using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GameManager : MonoBehaviour
{
    public static SCR_GameManager instance;

    public int score;

    public void Start()
    {
        if (SCR_GameManager.instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        instance = this;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartScene()
    {
        ChangeScene("SCE_Start");
    }

    public void EndScene()
    {
        ChangeScene("SCE_End");
    }
}
