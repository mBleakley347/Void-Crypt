using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Exit : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        SCR_GameManager.instance.EndScene();
    }
}
