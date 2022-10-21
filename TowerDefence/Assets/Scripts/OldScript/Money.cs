


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    
    private GameObject GameManager;
    private PlayerStats playerstats;
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + playerstats.Money.ToString();
    }
}
