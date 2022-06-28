using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float EnemyHealth;
    [SerializeField]
    private float MovementSpeed;

    private int KillReward;
    
    private int Damage;

    private GameObject targetTile;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        targetTile = RandomPath.startTile;
    }

    public void TakeDamage(float amount)
    {
        EnemyHealth -= amount;
        if (EnemyHealth <= 0)
        {
            die();    
        }
    }

    private void die()
    {
        Destroy(transform.gameObject);
        // Change this to object pooling instead of deleting obj - better performance
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position,
            MovementSpeed * Time.deltaTime);
    }

    private void CheckPos()
    {
        if(targetTile != null && targetTile != RandomPath.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.001f)
            {
                int currentIndex = RandomPath.pathTiles.IndexOf(targetTile);

                targetTile = RandomPath.pathTiles[currentIndex + 1];
            }
        }
        
        else if (targetTile == RandomPath.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;
            if (distance < 0.001f)
            {
                DoDamage();
                die();
            }
        }
    }

    private void DoDamage()
    {
        print("EndTileReached");
        
        // Remove life from players base
    }

    private void Update()
    {
        CheckPos();
        MoveEnemy();

        TakeDamage(0);
    }
}
