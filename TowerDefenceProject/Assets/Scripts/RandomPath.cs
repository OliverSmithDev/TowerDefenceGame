using System;
using System.Collections;
using System.Collections.Generic;
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

  public bool ReGen;
  private void Start()
  {
     generateMap();
  }

  private void Update()
  {
     if (ReGen == true)
     {
        print("Regenerating");
        ClearMap();
        ReGen = false;
     }

     MapTiles2 = mapTiles;

  }
  private void MoveDown()
  {
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-mapWidth;
     //mapTiles.Remove(currentTile);
     currentTile = mapTiles[NextIndex];
  }

  private void MoveLeft()
  {
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-1;
     //mapTiles.Remove(currentTile);
     currentTile = mapTiles[NextIndex];
  }

  private void MoveRight()
  {
     currentTile.tag = "Path";
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex+1;
     currentTile = mapTiles[NextIndex];
  }
  private void generateMap()
   {
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
            MoveLeft();
            
            Counter++;
            if (Counter == Random.Range(1,3))
            {
               MoveDown();
               print("movingDown");
               Counter = 0;
            }
            print("movingLeft");
            
         }
         else if (currentTile.transform.position.x < endTile.transform.position.x)
         {
            MoveRight();
            
            Counter++;
            if (Counter == Random.Range(1,3))
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
         obj.GetComponent<MeshRenderer>().material = pathColor;
      }
     // mapTiles.Remove(startTile);
      startTile.GetComponent<MeshRenderer>().material = startTileMaterial;
      endTile.GetComponent<MeshRenderer>().material = endTileMaterial;

     
      foreach (GameObject Object in mapTiles.ToList())
      {
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

      
   }

  private List<GameObject> getTopEdgeTiles()
  {
     List<GameObject> edgeTiles = new List<GameObject>();

     for (int i = mapWidth * (mapHeight-1); i < mapWidth * mapHeight; i++)
     {
        edgeTiles.Add(mapTiles[i]);
     }
     return edgeTiles;
  }
  private List<GameObject> getBottomEdgeTiles()
  {
     List<GameObject> edgeTiles = new List<GameObject>();

     for (int i = 0; i < mapWidth; i ++)
     {
        edgeTiles.Add(mapTiles[i]);
     }
     return edgeTiles;
     
  }

  
  public void ClearMap()
  {
     /*
     foreach (GameObject OBJ in pathTiles)
     {
        if (mapTiles.Contains(OBJ))
        {
           print("RemovedObj");
           mapTiles.Remove(OBJ);
        }
     }*/

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

        //Compare lists remove all duplicates from maptiles

    

        pathTiles.Clear();
        mapTiles.Clear();
        ReachedX = false;
        ReachedY = false;
        generateMap();
     }
  }


