using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ButtonManager : MonoBehaviour
{
    public void Scene(string name)
    {
        SCR_GameManager.instance.ChangeScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
