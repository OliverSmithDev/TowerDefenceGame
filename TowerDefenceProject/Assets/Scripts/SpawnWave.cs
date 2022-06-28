using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] Enemies;
    private int Index;
    public GameObject CurrentlySpawning;
    
    private int waveIndex = 0;

    public GameObject spawnPoint;

    public bool Test1 = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaveSpawn());
        //spawnPoint = RandomPath.startTile;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = RandomPath.startTile;
        if (Test1 == true)
        {
            StartCoroutine(WaveSpawn());
            Test1 = false;
        }
    }
    
    IEnumerator WaveSpawn()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

      
    }

    void SpawnEnemy()
    {
        Index = Random.Range(0, Enemies.Length);
        CurrentlySpawning = Enemies[Index];
        
        
        Instantiate(CurrentlySpawning, spawnPoint.transform.position, spawnPoint.transform.rotation); 
    }
}
