using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    public static int baseHealth;

    public static int EnergyProduced;

    public static int EnergyUsed;


    // Start is called before the first frame update
    void Start()
    {
        baseHealth = 20;
        EnergyProduced = 50;
    }

    // Update is called once per frame
    void Update()
    {

        if (EnergyProduced <= EnergyUsed)
        {
            print("ProducingLessEnergyThenUsed");
        }

        if (baseHealth <= 0)
        {
            GameOver();
        }
        
    }

    public void GameOver()
    {
        //EndGame
    }
    
    
}
