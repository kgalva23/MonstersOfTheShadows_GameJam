using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerupsSpawn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] List <GameObject> powerupObjects;
    [SerializeField] float timerInterval = 4f;

    public float gameTimer;
    public int counter = 0;

    void FixedUpdate() 
    {
        gameTimer += Time.deltaTime;    //set gameTimer to keep track of 

        int waveNum = gameManager.GetWave();

        //spawn objects every time interval is met, then reset timer
        if (gameTimer >= timerInterval && counter < waveNum) 
        {
            spawnPowerup();
            gameTimer = 0;
            counter++;
        }
    }

    public void ResetPowerupCounter()
    {
        counter = 0;    // Reset the counter for each new wave
    }

    public void spawnPowerup() {
        float randomPos = Random.Range (-16.0f, 16.0f);           //random starting position for objects
        int randomNum = Random.Range(0, powerupObjects.Count);  //random object from powerupObjects list

        //Instatiate a powerup object at a random postion
        GameObject randomPowerupObject = Instantiate(powerupObjects[randomNum], new Vector3(randomPos, randomPos, 0f), Quaternion.identity);
    }
}
