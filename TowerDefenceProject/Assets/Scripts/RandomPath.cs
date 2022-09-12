using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class RandomPath : MonoBehaviour
{
   public GameObject mapTile;

  [SerializeField] private int mapWidth; 
  [SerializeField] private int mapHeight;


  public static List<GameObject> mapTiles = new List<GameObject>();
  public static List<GameObject> pathTiles = new List<GameObject>();

  public List<GameObject> MapTiles2;
  [SerializeField]
  public static GameObject startTile;
  public static GameObject endTile;
  
  private bool ReachedX = false;
  private bool ReachedY = false;
  private GameObject currentTile;
  private int currentIndex;
  private int NextIndex;

  public Material pathColor;
  public Material DefaultMat;
  public Material startTileMaterial;
  public Material endTileMaterial;

  private int index;
  private GameObject CurrentlySpawning;
  public List<GameObject> Obstacles;
  public List<GameObject> SpawnedObjectList;
  public int SpawnableObjs;

  
  public List<GameObject> Enemys;

  public Node nodes;
  public bool ReGen;
  public bool CanGen;
  
  public GameObject MapMenu;
  private void Start()
  {
     //generateMap();
  }

  private void Update()
  {

     mapHeight = TextInRuntime.MapHeight;
     mapWidth = TextInRuntime.MapWidth;
     if (ReGen == true)
     {
        print("Regenerating");
        ClearMap();
        ReGen = false;
     }

     if (CanGen == true)
     {
        
        generateMap();
        CanGen = false;

     }
     MapTiles2 = mapTiles;

  }
  private void MoveDown()
  {
     // makes the path go down and adds that new tile to the pathtile and gives tag path
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-mapWidth;
     //mapTiles.Remove(currentTile);
     currentTile = mapTiles[NextIndex];
  }

  private void MoveLeft()
  {
     // makes the path go left and adds that new tile to the pathtile and gives tag path
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-1;
     //mapTiles.Remove(currentTile);
     currentTile = mapTiles[NextIndex];
  }

  private void MoveRight()
  {
     // makes the path go right and adds that new tile to the pathtile and gives tag path
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex+1;
     currentTile = mapTiles[NextIndex];
  }
  private void generateMap()
   {
      // tile generation

      if (MapMenu.activeInHierarchy)
      {
         MapMenu.SetActive(false);
      }
      
      else if (MapMenu.activeInHierarchy)
      {
         
      }
      
      for (int z = 0; z < mapHeight; z++)
      {
         for (int x = 0; x < mapWidth; x++)
         {
            GameObject newTile = Instantiate(mapTile);
            mapTiles.Add(newTile);

            newTile.transform.position = new Vector3(x, 0, z);
         }
      }

      List<GameObject> topEdgeTiles = getTopEdgeTiles();
      List<GameObject> BottomEdgeTiles = getBottomEdgeTiles();

      

      int rand1 = Random.Range(0, mapWidth);
      int rand2 = Random.Range(0, mapWidth);

      startTile = topEdgeTiles[rand1];
      
      endTile = BottomEdgeTiles[rand2];
     

      currentTile = startTile;

     
      MoveDown();

      int loopcount = 0;
      int Counter = 0;

      while (!ReachedX)
      {
         loopcount++;
         if (loopcount > 100)
         {
            print("LoopBroken");
            break;
         }
         if (currentTile.transform.position.x > endTile.transform.position.x)
         {
            // if end pos is to the left move to the left
            
            MoveLeft();
            
            Counter++;
            if (Counter == Random.Range(1,3))
               // randomly picks between 1,3 and moves down if it meets that number
            {
               MoveDown();
               print("movingDown");
               Counter = 0;
            }
            print("movingLeft");
            
         }
         else if (currentTile.transform.position.x < endTile.transform.position.x)
         {
            // if end pos is to the left move to the right
            
            MoveRight();
            
            Counter++;
            if (Counter == Random.Range(1,3))
               // randomly picks between 1,3 and moves down if it meets that number
            {
               MoveDown();
               print("movingDown");
               Counter = 0;
            }
            print("movingRight");
         }

         else
         {
            ReachedX = true;
            print("ReachedX");
         }
      }

      while (!ReachedY)
      {
         // checks if it is on same Z level and if not moves down
         
         if (currentTile.transform.position.z > endTile.transform.position.z)
         {
          MoveDown();
          print("movingDown");
         }
         else
         {
            ReachedY = true;
         }
      } 
      pathTiles.Add(endTile);
      mapTiles.Remove(endTile);
      // pathTiles.Add(startTile);

      foreach (GameObject obj in pathTiles)
      {
         obj.GetComponent<MeshRenderer>().material = pathColor; // sets path colour
      }

      startTile.GetComponent<MeshRenderer>().material = startTileMaterial;
      endTile.GetComponent<MeshRenderer>().material = endTileMaterial;

     
      foreach (GameObject Object in mapTiles.ToList())
      {
         // if tag is path remove the object from mamptiles
         
         if (Object.CompareTag("Path"))
         {
            if (mapTiles.Count > 1)
            {
               mapTiles.Remove(Object);
            }
         }
      }
      
      foreach (var x in pathTiles)
      { 
         Debug.Log(x.ToString());
      }
      foreach (var x in mapTiles)
      { 
         Debug.Log(x.ToString());
      }
      
      
      // Spawn obstacles on the map 

      index = Random.Range(0, Obstacles.Count);
      CurrentlySpawning = Obstacles[index];

      SpawnableObjs = mapTiles.Count / 2;

      for (int i = 0; i < SpawnableObjs; i++)
      {
         index = Random.Range(0, Obstacles.Count);
         CurrentlySpawning = Obstacles[index];
         int spawnIndex = Random.Range(0, mapTiles.Count);
         GameObject SpawnOBJ = Instantiate(CurrentlySpawning, mapTiles[spawnIndex].transform.position,
            mapTiles[spawnIndex].transform.rotation);
         SpawnedObjectList.Add(SpawnOBJ);
         Destroy(mapTiles[spawnIndex]);
         mapTiles.Remove(mapTiles[spawnIndex]);
         
      }


      foreach (GameObject path in pathTiles)
      {
         //nodes = GetComponent<Node>();
         //Destroy (GetComponent<Node>());
         path.GetComponent<Node>().enabled = false;
         print("RemovedNode");
         
      }

      SpawnWave.Test1 = true;
      

   }

  private List<GameObject> getTopEdgeTiles()
  {
     // Find top row tiles
     
     List<GameObject> edgeTiles = new List<GameObject>();

     for (int i = mapWidth * (mapHeight-1); i < mapWidth * mapHeight; i++)
     {
        edgeTiles.Add(mapTiles[i]);
     }
     return edgeTiles;
  }
  private List<GameObject> getBottomEdgeTiles()
  {
     // Find bottom row tiles
     
     List<GameObject> edgeTiles = new List<GameObject>();

     for (int i = 0; i < mapWidth; i ++)
     {
        edgeTiles.Add(mapTiles[i]);
     }
     return edgeTiles;
     
  }

  
  public void ClearMap()
  {

      // Clear all lists and destroy all objects in scene - then rerun generatemap function
   
     foreach (GameObject obj in SpawnedObjectList)
     {
        Destroy(obj);
        print("DestroyedObstacles");
     }
     foreach (GameObject obj in mapTiles)
     {
           Destroy(obj);
           print("DestroyedMapTiles");
     }
     
     foreach (GameObject obj in pathTiles)
     {
           Destroy(obj);
           print("DestroyedPathTile");
     }
     
     
     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
     foreach(GameObject enemy in enemies)
        GameObject.Destroy(enemy);

     GameObject[] Towers = GameObject.FindGameObjectsWithTag("Tower");
     foreach(GameObject tower in Towers)
        GameObject.Destroy(tower);
     
     SpawnWave.waveIndex = 0;

     Base.EnergyProduced = 50;
     Base.EnergyUsed = 0;
     Base.baseHealth = 20;

        pathTiles.Clear();
        mapTiles.Clear();
        SpawnedObjectList.Clear();
        ReachedX = false;
        ReachedY = false;
        generateMap();
     }
  }


