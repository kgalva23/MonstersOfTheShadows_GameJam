using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturnManager : MonoBehaviour
{
    //Return player back to Main Menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1; // Resume game speed
        SceneManager.LoadScene("MainMenu");
    }

    //Replay Level for Player
    public void ReplayLevel()
    {
        Time.timeScale = 1; // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
