using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSun : MonoBehaviour
{
    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene(0);
    }
}
