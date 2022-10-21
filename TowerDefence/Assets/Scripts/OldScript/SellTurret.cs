using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTurret : MonoBehaviour
{

    public float startTime = 0f;
    public float endTime = 0f;
    public bool CanSell = false;
    public int value = 50;
    private GameObject GameManager;
    private PlayerStats playerstats;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerstats = GameManager.GetComponent<PlayerStats>();
        startTime = 0f;
        endTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
        }

        if(endTime - startTime > 0.5f)
        {
            Destroy(gameObject);
            playerstats.Money += value;
            startTime = 0f;
            endTime = 0f;
        }
    }
}
