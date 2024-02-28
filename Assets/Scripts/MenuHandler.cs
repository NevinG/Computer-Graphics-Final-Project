using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void ClickStart()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
