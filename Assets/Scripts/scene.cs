using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene : MonoBehaviour
{
    public void Movescene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
