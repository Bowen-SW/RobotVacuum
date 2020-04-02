using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Globalization;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    [HideInInspector] public List<Room> rooms;
    [HideInInspector] public List<Chest> chests = UserInputInformation.chests;
    [HideInInspector] public List<Chest> chairs = UserInputInformation.chairs;
    [HideInInspector] public List<RunReport> reports = new List<RunReport>();
    [HideInInspector] public List<Vector2> startValues;
    [HideInInspector] public List<Vector2> stopValues;
    [HideInInspector] public string fileName;
    [HideInInspector] public int totalSqft;
    [HideInInspector] public static DateTime timeStamp;
    [HideInInspector] public static string timeStampString;
    [HideInInspector] public static string duration;
    [HideInInspector] public static string carpetType;
    [HideInInspector] public static string pathType;
    [HideInInspector] public static int timeStampCount = 0;
    [HideInInspector] public static int coverage;

    public void setTotalSqft()
    {
        totalSqft = 0;
        for(int i = 0; i < startValues.Count; i++)
        {
            int width = ((int)(stopValues[i].x - startValues[i].x));
            int height = ((int)(stopValues[i].y - startValues[i].y));
            totalSqft += (width*height);
        }
    }

    public void setTimeStamp()
    {
        if(timeStampCount == 0)
        {
            timeStamp = DateTime.Now;
            timeStampString = timeStamp.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo);
            timeStampCount++;
        }
    }

    public void recordRun()
    {
        carpetType = UserInputInformation.carpetType;
        duration = UserInputInformation.durationGS;
        pathType = UserInputInformation.pathTypeGS;
        coverage = 0;
        RunReport newRun = new RunReport(timeStampString, duration, pathType, coverage);
        Debug.Log(newRun);
        reports.Add(newRun);
    }

    public void setFurniture()
    {
        rooms = new List<Room>();
        foreach(GameObject room in UserInputInformation.rooms)
        {
            rooms.Add(room.GetComponent<Room>());
        }
        startValues = UserInputInformation.startVals;
        stopValues = UserInputInformation.stopVals;
    }

    public void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", Application.persistentDataPath, "json");
    }

    public void Save()
    {
        setTimeStamp();
        setTotalSqft();
        setFurniture();
        recordRun();
        recordRun();
        recordRun();
    	fileName = UserInputInformation.FileNameGS;
        Debug.Log(JsonUtility.ToJson(this));
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(this));
    }
}
