using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;
using System;
using System.Globalization;
using LitJson;

public static class Loader

{
    public static string openFileFromBrowser()
    {
        string filePath = "";
        try
        {
            string initialDir = @"C:\RobotVacuumFiles";
            bool restoreDir = true;
            string title = "Open a JSON File";
            string defExt = "json";
            string filter = "json files (*.json)|*.json";

            SmartDLL.SmartFileExplorer fileExplorer = new SmartDLL.SmartFileExplorer();
            fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);

            filePath = fileExplorer.fileName;
        }
        catch
        {
            Debug.Log("Invalid File.");
        }
        Debug.Log(filePath + " chosen");
        return filePath;
    }

    public static bool validateFileFields(JsonData saveInformation)
    {
        bool fieldsExist = true;
        try
        {
            JsonData test1 = saveInformation["rooms"];
            JsonData test2 = saveInformation["doors"];
            JsonData test3 = saveInformation["chests"];
            JsonData test4 = saveInformation["chairs"]; 
            JsonData test6 = saveInformation["reports"];
        }
        catch(Exception exception)
        {
            if(exception is KeyNotFoundException)
            {
                Debug.Log("Error loading file:");
                Debug.Log("JSON file does not contain the correct keys.");
                fieldsExist = false;
            }
        }
        return fieldsExist;
    }

    public static bool checkRooms(JsonData saveInformation)
    {
        if(saveInformation["rooms"].Count == 0)
        {
            return false;
        }
        return true;
    }

    public static bool checkDoors(JsonData saveInformation)
    {
        if(saveInformation["doors"].Count == 0)
        {
            return false;
        }
        return true;
    }

    public static bool checkChairs(JsonData saveInformation)
    {
        if(saveInformation["chairs"].Count == 0)
        {
            return false;
        }
        return true;
    }

    public static bool checkChests(JsonData saveInformation)
    {
        if(saveInformation["chests"].Count == 0)
        {
            return false;
        }
        return true;
    }

    public static bool checkReports(JsonData saveInformation)
    {
        if(saveInformation["reports"].Count == 0)
        {
            return false;
        }
        return true;    
    }

}