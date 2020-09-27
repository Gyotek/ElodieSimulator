using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BAD_Echap : MonoBehaviour
{
    public void Echap()
    {
       SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Echap();
        }
    }
}
