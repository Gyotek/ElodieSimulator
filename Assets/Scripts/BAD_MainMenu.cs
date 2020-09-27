using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BAD_MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(2);

    }

    public void PlayCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Restart ()
    {
        SceneManager.LoadScene(2);

    }


    
}
