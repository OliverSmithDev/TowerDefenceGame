using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPath : MonoBehaviour
{
   public GameObject mapTile;

  [SerializeField] private int mapWidth; 
  [SerializeField] private int mapHeight;


  private List<GameObject> mapTiles = new List<GameObject>();
  private List<GameObject> pathTiles = new List<GameObject>();

  private bool ReachedX = false;
  private bool ReachedY = false;
  private GameObject currentTile;
  private int currentIndex;
  private int NextIndex;

  public Material pathColor;
  private void Start()
  {
     generateMap();
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

  private void MoveDown()
  {
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-mapWidth;
     currentTile = mapTiles[NextIndex];
  }

  private void MoveLeft()
  {
     pathTiles.Add(currentTile);
     currentIndex = mapTiles.IndexOf(currentTile);
     NextIndex = currentIndex-1;
     currentTile = mapTiles[NextIndex];
  }

  private void MoveRight()
  {
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

      GameObject startTile;
      GameObject endTile;

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
      pathTiles.Add((endTile));

      foreach (GameObject obj in pathTiles)
      {
         obj.GetComponent<MeshRenderer>().material = pathColor;
      }
   }

}
