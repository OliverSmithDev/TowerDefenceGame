﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   

    private Transform target;

    public int health = 100;
    public int value = 10;

    [SerializeField]
    private float MovementSpeed;

    public int KillReward;
    
    public int Damage;

    public GameObject targetTile;

    public static int DamageInt;
    
    private GameObject GameManager;
    private PlayerStats playerstats;

    
    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerstats = GameManager.GetComponent<PlayerStats>();
       SpawnEnemy();
    }

    
   
    private void SpawnEnemy()
    {
        targetTile = RandomPath.startTile;
    }
    
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerstats.Money += value;
        Debug.Log("MoneyGiven");
        Destroy(gameObject);
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
                Die();
            }
        }
    }
    private void DoDamage()
    {
        print("EndTileReached");

        Base.baseHealth -= Damage;
        print(Base.baseHealth);

        // Remove life from players base
    }
    
    private void Update()
    {
        CheckPos();
        MoveEnemy();

        TakeDamage(0);
    }

}
