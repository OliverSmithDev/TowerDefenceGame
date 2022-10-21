using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject BaseTowerObject;
    private GameObject dummyPlacement;
    public Camera cam;


    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Update()
    {
        Debug.Log(GetMousePosition());
    }
}
