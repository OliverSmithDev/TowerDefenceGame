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
        if (MapHeight >= 20)
        {
            MapHeight = 20;
        }
        if (MapWidth >= 20)
        {
            MapWidth = 20;
        }
    }

    public void ReadStringInput(string s)
    {
        MapHeightString = s;
        Debug.Log(MapHeightString);
        
        MapHeight = Int32.Parse(MapHeightString);
        MapWidth = Int32.Parse(MapHeightString);

    }

}
