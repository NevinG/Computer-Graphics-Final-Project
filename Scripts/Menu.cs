using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject go;
    public void OnMouseUpAsButton()
    {
        go.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoResume()
    {
        Time.timeScale = 1;
        go.SetActive(false);
    }
}
