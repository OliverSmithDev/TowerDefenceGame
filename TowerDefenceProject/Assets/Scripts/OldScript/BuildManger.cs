using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManger : MonoBehaviour
{
    public static BuildManger instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one buildmanger in scene");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject UpgradedTurretPrefab;
    public GameObject MiniGunTurretPrefab;
    public GameObject SniperTurretPrefab;

    private TurretBluePrint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
       if (PlayerStats.Money < turretToBuild.cost || Base.EnergyProduced <= Base.EnergyUsed)
        {
            Debug.Log("No Money");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        Base.EnergyUsed += turretToBuild.EnergyCost;
        print(Base.EnergyUsed);

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret bought! Money left:" + PlayerStats.Money);
    }

    public void SelectTurretToBuild ( TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
