using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Text;

public class NewFloorPlanMenuManager : MonoBehaviour
{
    public GameObject menu;

    public TextMeshProUGUI errorMessage;
    public TextMeshProUGUI fileName;
    public TextMeshProUGUI roomWidth;
    public TextMeshProUGUI roomHeight;
    public TextMeshProUGUI carpetType;
    public TestSimScript simScript;

    // Start is called before the first frame update
    void Start()
    {
        simScript = GetComponentInParent<TestSimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData()
    {

    }

    public bool checkSqft(string heightS, string widthS)
    {
        int width = int.Parse(widthS);
        int height = int.Parse(heightS);
        if (width * height > 8000)
        {
            errorMessage.text = "Err: Area must be < 8000 sq. ft.";
            return false;
        }
        else 
        {
            return true;
        }

    }

    public bool checkFileName()
    {
        if (fileName.text.Replace("\u200B", "") == "")
        {
            errorMessage.text = "Err: Enter a file name.";
            return false;
        }
        else 
        {
            return true;
        }
    }

    public void Close()
    {

        // if(checkFileName(fileName.text.Replace("\u200B", "")) == false)
        // {
        //     checkFileName(fileName.text.Replace("\u200B", ""));
        // }

        // if(checkSqft(roomHeight.text.Replace("\u200B", ""), roomWidth.text.Replace("\u200B", "")) == false)
        // {
        //     checkSqft(roomHeight.text.Replace("\u200B", ""), roomWidth.text.Replace("\u200B", ""));
        // }

        UserInputInformation.FileNameGS = fileName.text.Replace("\u200B", "");
        UserInputInformation.carpetTypeGS = carpetType.text.Replace("\u200B", "");

        Destroy(menu);
    }
}
