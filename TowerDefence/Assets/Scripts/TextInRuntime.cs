using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInRuntime : MonoBehaviour
{
    private string MapHeightString;
    private string MapWidthString;
    public static int MapHeight;
    public static int MapWidth;
    
    public GameObject MapGen;

    public InputField IfieldHeight;

    public InputField IfieldWidth;
    // Start is called before the first frame update
    void Start()
    {
        IfieldHeight.characterValidation = InputField.CharacterValidation.Integer;
        IfieldWidth.characterValidation = InputField.CharacterValidation.Integer;
    }

    // Update is called once per frame
    void Update()
    {
        if (MapHeight >= 25)
        {
            MapHeight = 25;
        }
        if (MapWidth >= 25)
        {
            MapWidth = 25;
        }

        if (MapHeight <= 10)
        {
            MapHeight = 10;
        }

        if (MapWidth <= 10)
        {
            MapWidth = 10;
        }
        
      
        
    }

    public void ReadStringInput(string s)
    {
        MapHeightString = s;
        Debug.Log(MapHeightString);
        MapHeight = Int32.Parse(MapHeightString);
        
    }

    public void ReadStringInputWidth(string W)
    {
        MapWidthString = W;
        Debug.Log(MapWidthString);
        MapWidth = Int32.Parse(MapWidthString);
    }
}
