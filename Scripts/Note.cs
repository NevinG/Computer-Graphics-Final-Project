using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    public GameObject readableNote;
    public void OnMouseUpAsButton()
    {
        readableNote.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
