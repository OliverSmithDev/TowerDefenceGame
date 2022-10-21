using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint UpgradedTurret;
    public TurretBluePrint MiniGunTurret;
    public TurretBluePrint SniperTurret;

    BuildManger buildManger;

   

    private void Start()
    {
        buildManger = BuildManger.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManger.SelectTurretToBuild(standardTurret);
    }

    public void SelectUpgradedTurret()
    {
        Debug.Log("Upgraded Turret Purchased");
        buildManger.SelectTurretToBuild(UpgradedTurret);
    }

    public void SelectMiniGunTurret()
    {
        Debug.Log("MiniGun Turret Purchased");
        buildManger.SelectTurretToBuild(MiniGunTurret);
    }

    public void SelectSniperTurret()
    {
        Debug.Log("Sniper Turret Purchased");
        buildManger.SelectTurretToBuild(SniperTurret);
    }
}



