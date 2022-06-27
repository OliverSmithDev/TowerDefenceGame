﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemyPrefab;
    public Transform BossPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 0f;
    public static int WaveLevel;
    public int WaveLevelMax = 0;

    public Text WaveCountdownText;

    private int waveIndex = 0;

    public bool bossspawn = false;
    public bool hasSpawned = false;
    public bool countdowntrue = true;
    public bool cansettime = true;


    private void Update()
    {

        if(GameObject.FindWithTag("Enemy") == null)
        {
            countdowntrue = true;
            //print("spawn");
        }
        if (GameObject.FindWithTag("Enemy"))
        {
            countdowntrue = false;
            //print("Nospawn");
        }
        if (countdowntrue == true && cansettime == true)
        {
            countdown = timeBetweenWaves;
            cansettime = false;
        }


        if (countdowntrue == true && countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdowntrue = false;
            cansettime = true;
            WaveLevel += 1;
            WaveLevelMax += 1;
            //print("Wave");



            if (WaveLevel >= 21)
            {
                print("Finished");
                SceneManager.LoadScene("EndScene");
            }

        }
        if(hasSpawned == false && WaveLevel == 5)
        {
            bossspawn = true;
        }
        if (hasSpawned == false && WaveLevel == 10)
        {
            bossspawn = true;
        }
        if (hasSpawned == false && WaveLevel == 15)
        {
            bossspawn = true;
        }
        if (hasSpawned == false && WaveLevel == 20)
        {
            bossspawn = true;
        }


        if (bossspawn == true && WaveLevel == 5)
        {
            Instantiate(BossPrefab, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true;
            bossspawn = false;
            
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        WaveCountdownText.text = string.Format("{0:00.00}", countdown);

       

    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

      
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); 
    }

}
