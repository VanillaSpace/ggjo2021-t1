using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public void onExit()
    {
        Application.Quit();
    }

    public void onStart()
    {
        SceneManager.LoadScene("Scene1");
    }
}
