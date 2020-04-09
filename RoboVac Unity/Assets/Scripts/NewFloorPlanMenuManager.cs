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
    public RoombaSettingsScript roombaSettings;
    public int warningCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        roombaSettings = GetComponentInParent<RoombaSettingsScript>();
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
            else if (width * height < 4)
            {
                errorMessage.text = "Err: Area must be >= 4 sq. ft.";
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
                UserInputInformation.overwriteWarningGS = warningCount;
                warningCount--;
                return true;
            }      
        }
        else 
        {
            return true;
        }
    }

    void clearLists()
    {
        foreach(GameObject room in UserInputInformation.rooms)
        {
            Destroy(room);
        }
        UserInputInformation.rooms.Clear();
        
        // foreach(GameObject table in UserInputInformation.tables)
        // {
        //     Destroy(table);
        // }
        // UserInputInformation.tables.Clear();

        // foreach(GameObject chest in UserInputInformation.chests)
        // {
        //     Destroy(chest);
        // }
        // UserInputInformation.chests.Clear();

        // foreach(GameObject chair in UserInputInformation.chair)
        // {
        //     Destroy(chair);
        // }
        // UserInputInformation.chairs.Clear();
        UserInputInformation.stopVals.Clear();
        UserInputInformation.startVals.Clear();
    }

    public void Accept()
    {

        roombaSettings.SetFloorType(carpetType.text);

        string roomHeightStripped = roomHeight.text.Replace("\u200B", "");
        string roomWidthStripped = roomWidth.text.Replace("\u200B", "");

        if((checkFileName() == true) && (checkSqft(roomHeightStripped, roomWidthStripped) == true))
        {
            UserInputInformation.FileNameGS = fileName.text.Replace("\u200B", "");
            UserInputInformation.carpetTypeGS = carpetType.text.Replace("\u200B", "");
            clearLists(); // on creation of a new file, all of the old information must be erased from the current scene

            GameObject newRoom = Instantiate(objectToAdd, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            //UserInputInformation.AddRoom((GameObject)Instantiate(objectToAdd, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));

            if(roomHeightStripped != "" && roomWidthStripped != "")
            {
                // Set the Width and Height from what the user entered
                int width = int.Parse(roomHeightStripped);
                int height = int.Parse(roomWidthStripped);

                // Create the new default room with the user specified dimensions
                newRoom.GetComponent<Room>().LoadPositions(new Vector2(-((float)width / 2f), -((float)height / 2f)), new Vector2((float)width / 2f, (float)height / 2f));
            }
            UserInputInformation.AddRoom(newRoom);
            Close();
        }
    }
    public void Close()
    {
        Destroy(menu);
    }
}
