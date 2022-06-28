using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;
    private Color startColor;
    public Vector3 positionOffset;

    [Header ("Optional")]
    public GameObject turret;
    

    BuildManger buildManger; 
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManger = BuildManger.instance;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; 
    }

    private void OnMouseDown()
    {
        if (!buildManger.CanBuild)
            return;
            

        if(turret != null)
        {
            Debug.Log("No go away");
            return;
        }

        buildManger.BuildTurretOn(this);

    }

    private void OnMouseEnter()
    {
        if (!buildManger.CanBuild)
            return;

        if (buildManger.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
       
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
