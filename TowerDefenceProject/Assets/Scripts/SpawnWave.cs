using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] Enemies;
    private int Index;
    public GameObject CurrentlySpawning;
    
    public static int waveIndex = 0;

    public GameObject spawnPoint;

    public static bool Test1 = false;

    public float Cooldown;

    public bool CanSpawn;
    
    public bool hasSpawned = false;
    public bool countdowntrue = true;
    public bool cansettime = true;
    public float timeBetweenWaves = 5f;
    [SerializeField]
    private float countdown = 0f;
    public static int WaveLevel;
    public int WaveLevelMax = 0;
    // Start is called before the first frame update
    void Start()
    {
        //SpawningWave();
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
        
        if(GameObject.FindWithTag("Enemy") == null)
        {
            countdowntrue = true;
            print("spawn");
        }
        if (GameObject.FindWithTag("Enemy") == true)
        {
            countdowntrue = false;
            print("Nospawn");
        }
        if (countdowntrue == true && cansettime == true)
        {
            countdown = timeBetweenWaves;
            cansettime = false;
        }


        if (countdowntrue == true && countdown <= 0f)
        {
            StartCoroutine(WaveSpawn());
            countdowntrue = false;
            cansettime = true;
            WaveLevel += 1;
            WaveLevelMax += 1;
            //print("Wave");
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }
        

    public void SpawningWave()
    {
        StartCoroutine(WaveSpawn());
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
