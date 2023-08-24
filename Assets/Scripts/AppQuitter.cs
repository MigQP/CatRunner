using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppQuitter : MonoBehaviour
{

    public void ExitGame()
    {
        Debug.Log("saliste");
        Application.Quit();
        
    }
}
