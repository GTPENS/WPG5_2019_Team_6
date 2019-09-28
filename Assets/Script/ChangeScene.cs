using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
