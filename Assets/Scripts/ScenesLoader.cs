using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{



    // Function to move to another scene
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadEvolution()
    {
        SceneManager.LoadScene("Evolutions");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
    

