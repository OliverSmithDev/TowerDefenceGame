using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    
    public int Money;
    public int startMonney = 400;
    public static int Lives;
    public int startLives = 20;
    public TMP_Text moneyText;
    [SerializeField] public float energyTotal;
    [SerializeField] public float energyUssage;
    private void Start()
    {
        Money = startMonney;
        Lives = startLives;
    }

    public void Update()
    {

        if(Lives <= 0)
        {
            SceneManager.LoadScene("EndScene");
            //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        moneyText.text = Money.ToString();

        if (energyUssage >= energyTotal)
        {
            print("NotEnoughEnergy");
        }
        

    }
}
