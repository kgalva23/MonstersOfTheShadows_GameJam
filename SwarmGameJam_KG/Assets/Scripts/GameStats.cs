using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStats : MonoBehaviour
{
    public TextMeshProUGUI gameStats;             // Reference to GameStats text UI

    public void DisplayGameStats()
    {
        // Update the total score text
        int finalScore = GameManager.instance.score;

        // Update the waves survived text
        int wavesSurvived = (GameManager.instance.wave) - 1;

        // Update the Completion Time text
        float survivalTime = GameTimer.Instance.GetElapsedTime();

        gameStats.text = "Final Score: " + finalScore + "\nWaves Survived: " + wavesSurvived + "\nSurvival Time: " + survivalTime.ToString("F2") + "s";
    }

}
