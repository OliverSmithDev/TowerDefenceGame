using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManger : MonoBehaviour
{
    public static BuildManger instance;
    private GameObject GameManager;
    public PlayerStats playerstats;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one buildmanger in scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerstats = GameManager.GetComponent<PlayerStats>();
    }

    public GameObject standardTurretPrefab;
    public GameObject UpgradedTurretPrefab;
    public GameObject MiniGunTurretPrefab;
    public GameObject SniperTurretPrefab;

    private TurretBluePrint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return playerstats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
       if (playerstats.Money < turretToBuild.cost || Base.EnergyProduced <= Base.EnergyUsed)
        {
            Debug.Log("No Money");
            return;
        }

        playerstats.Money-= turretToBuild.cost;
        playerstats.energyUssage += turretToBuild.EnergyCost;
        print(playerstats.energyUssage);

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret bought! Money left:" + playerstats.Money);
    }

    public void SelectTurretToBuild ( TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
