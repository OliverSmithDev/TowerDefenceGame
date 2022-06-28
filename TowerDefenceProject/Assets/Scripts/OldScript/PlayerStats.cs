using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMonney = 400;

    public static int Lives;
    public int startLives = 20;

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


    }
}
