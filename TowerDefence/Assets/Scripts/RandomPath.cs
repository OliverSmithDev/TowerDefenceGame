using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class RandomPath : MonoBehaviour
{
   public GameObject mapTile;

  [SerializeField] private int mapWidth; 
  [SerializeField] private int mapHeight;


  private static List<GameObject> mapTiles = new List<GameObject>();
  public static List<GameObject> pathTiles = new List<GameObject>();

  public List<GameObject> MapTiles2;
  [SerializeField]
  public static GameObject startTile;
  public static GameObject endTile;
  
  private bool ReachedX = false;
  private bool ReachedY = false;
  [SerializeField] private GameObject currentTile;
  private int currentIndex;
  private int NextIndex;
  
  public Material[] randomMaterials;
  public Material pathColor;
  public Material DefaultMat;
  public Material startTileMaterial;
  public Material endTileMaterial;

  private int index;
  private GameObject CurrentlySpawning;
  public List<GameObject> Obstacles;
  public List<GameObject> SpawnedObjectList;
  public int SpawnableObjs;

  public bool canmoveleft;
  public bool canmoveright;
  

  
  public List<GameObject> Enemys;


  
  [SerializeField] private List<GameObject> EdgeTilesBottom = new List<GameObject>();
  [SerializeField] private List<GameObject> EdgeTilesTop = new List<GameObject>();
  [SerializeField] private List<GameObject> EdgeTilesLeft = new List<GameObject>();
  [SerializeField] private List<GameObject> EdgeTilesRight = new List<GameObject>();

  public Node nodes;
  public bool ReGen;
  public bool CanGen;
  private bool Generating;

  public GameObject MapMenu;
  private void Start()
  {
     Generating = false;
  }

  private void Update()
  {
     if (mapHeight == 0 || mapWidth == 0)
     {
        Time.timeScale = 0;
     }

     else
     {
        Time.timeScale = 1;
     }

     

     if (Generating)
     {
        
        if (currentTile.transform.position.x > endTile.transform.position.x)
        {
           ReachedX = false;
        }

        else if (currentTile.transform.position.x < endTile.transform.position.x)
        {
           ReachedX = false;
        }
        
     }
     


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
      Generating = true;
      if (MapMenu.activeInHierarchy)
      {
         MapMenu.SetActive(false);
      }
      else
      {
         MapMenu.SetActive(true);
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

      EdgeTilesBottom = getBottomEdgeTiles();
      EdgeTilesTop = getTopEdgeTiles();
      EdgeTilesRight = GetColumn(mapWidth - 1);
      EdgeTilesLeft = GetColumn(0);



      int rand1 = Random.Range(0, mapWidth);
      int rand2 = Random.Range(0, mapWidth);

      startTile = EdgeTilesTop[rand1];
      
      endTile = EdgeTilesBottom[rand2];
     

      currentTile = startTile;

     
      MoveDown();

      int loopcount = 0;
      int Counter = 0;
      int CounterDown = 0;

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
         if (EdgeTilesBottom.Contains(currentTile))
         {
            ReachedY = true;
         }
         
         // checks if it is on same Z level and if not moves down
         
         if (currentTile.transform.position.z > endTile.transform.position.z) // checks if the Z level is larger then the end tile
         {
            MoveDown();

            CounterDown++;
            

            int RandomNumber;
            int MoveAmount;

            
            // randomly picks between 1,3 and moves down if it meets that number
            RandomNumber = Random.Range(1, 3);
            MoveAmount = Random.Range(1, 5);
            if (CounterDown == RandomNumber)
            {
               bool GoLeft = (Random.value > 0.5); // checks if number is larger then 0.5 if so GoLeft = true

               if (GoLeft) // moves left 
               {
                  
                     for (int i = 0; i < MoveAmount; i++) // runs for loop for how many moves in MoveAmount
                     {
                        currentIndex = mapTiles.IndexOf(currentTile);
                        if ((currentIndex - (currentIndex % mapWidth) == (currentIndex-1) - ((currentIndex-1) % mapWidth)))
                        {
                           
                           print("movingleftD");
                           MoveLeft();
                        }
                        
                     }
                  
                  
               }

               else // moves right
               {
                  for (int i = 0; i < MoveAmount; i++) // Same as above
                  {
                     currentIndex = mapTiles.IndexOf(currentTile);
                     if ((currentIndex - (currentIndex % mapWidth) == (currentIndex+1) - ((currentIndex+1) % mapWidth)))
                     {
                        print("movingrightD");
                        MoveRight();
                     }
                     
                  }
               }
            }

            CounterDown = 0;
            
         }
         else
         {
            ReachedY = true;
         }
      } 
      
      
      endTile = currentTile;
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
         //Debug.Log(x.ToString());
      }
      foreach (GameObject Tile in mapTiles)
      {
         Tile.GetComponent<MeshRenderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
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
         
      
      //EdgeTilesLeft.Clear();
      //EdgeTilesLeft = getLeftEdgeTiles();
      //EdgeTilesRight.Clear();
     // EdgeTilesRight = getRightEdgeTiles();
      
     
      
      
      

      foreach (GameObject path in pathTiles)
      {
         path.GetComponent<Node>().enabled = false;
      }

      //SpawnWave.Test1 = true;
      SpawnWave.FirstWaveSpawn = true;
      Generating = false;
   }

  private List<GameObject> getTopEdgeTiles()
  {
     // Find top row tiles
     if (EdgeTilesTop.Count >= 0)
     {
        EdgeTilesTop.Clear();
     }
     

     for (int i = mapWidth * (mapHeight-1); i < mapWidth * mapHeight; i++)
     {
        EdgeTilesTop.Add(mapTiles[i]);
     }
     return EdgeTilesTop;
  }
  private List<GameObject> getBottomEdgeTiles()
  {
     // Find bottom row tiles
     

     for (int i = 0; i < mapWidth; i ++)
     {
        EdgeTilesBottom.Add(mapTiles[i]);
     }
     return EdgeTilesBottom;
     
  }
  
  public List<GameObject> GetColumn (int column)
  {
     // column count starts at 0
     int index = column;
     List<GameObject> returnList = new List<GameObject>();
     while(index < mapTiles.Count)
     {
        returnList.Add(mapTiles[index]);
        index += mapWidth;
     }

     return returnList;
  }
  
  private List<GameObject> getRightEdgeTiles()
  {
     // Find right tiles

     if (EdgeTilesRight.Count >= 0)
     {
        EdgeTilesRight.Clear();
     }
     
     for (int i = mapHeight * (mapWidth-1); i < mapHeight * mapWidth; i++)
     {
        EdgeTilesRight.Add(mapTiles[i]);
        
     }
     return EdgeTilesRight;
  }

  public void ClearMap()
  {

      // Clear all lists and destroy all objects in scene - then rerun generatemap function
   
     foreach (GameObject obj in SpawnedObjectList)
     {
        Destroy(obj);
  
     }
     foreach (GameObject obj in mapTiles)
     {
           Destroy(obj);
         
     }
     
     foreach (GameObject obj in pathTiles)
     {
           Destroy(obj);
           
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


