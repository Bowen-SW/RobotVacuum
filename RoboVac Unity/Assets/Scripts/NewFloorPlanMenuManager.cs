using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

public class NewFloorPlanMenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject objectToAdd;
    public TextMeshProUGUI errorMessage;
    public TextMeshProUGUI fileName;
    public TextMeshProUGUI roomWidth;
    public TextMeshProUGUI roomHeight;
    public TextMeshProUGUI carpetType;
    public TestSimScript simScript;
    public int warningCount = 0;

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
        if(heightS != "" && widthS != "")
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
        else
        {
            //accepting default room size if user does not specify
            return true;
        }


    }

    public bool checkFileName()
    {
        string fileNameStripped = fileName.text.Replace("\u200B", "");
        if (fileNameStripped == "")
        {
            errorMessage.text = "Err: Enter a file name.";
            return false;
        }
        // else if (Directory.Exists(Application.persistentDataPath+fileName.text.Replace("\u200B", "")+".json"))
        else if(File.Exists(Application.persistentDataPath + "/" + fileNameStripped + ".json"))
        {
            errorMessage.text = "Warning: Duplicate file name. Press accept to overwrite existing file.";
            if(warningCount == 0)
            {
                warningCount++;
                return false;
            }
            else
            {
                return true;
            }      
        }
        else 
        {
            return true;
        }
    }

    public void Close()
    {
        string roomHeightStripped = roomHeight.text.Replace("\u200B", "");
        string roomWidthStripped = roomWidth.text.Replace("\u200B", "");
        if((checkFileName() == true) && (checkSqft(roomHeightStripped, roomWidthStripped) == true))
        {
            UserInputInformation.FileNameGS = fileName.text.Replace("\u200B", "");
            UserInputInformation.carpetTypeGS = carpetType.text.Replace("\u200B", "");
            Destroy(menu);
            UserInputInformation.AddRoom((GameObject)Instantiate(objectToAdd, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        }
    }
}
