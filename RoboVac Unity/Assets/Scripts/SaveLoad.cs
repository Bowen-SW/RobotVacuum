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

    //public Roomba roomba;
    //public FloorPlan/Simulation (room aggregator) floorplan;

    [HideInInspector] public List<Room> rooms;
    [HideInInspector] public List<Chest> chests = UserInputInformation.chests;
    [HideInInspector] public List<Chest> chairs = UserInputInformation.chairs;
    [HideInInspector] public List<RunReport> reports = new List<RunReport>();
    [HideInInspector] public List<Vector2> startValues;
    [HideInInspector] public List<Vector2> stopValues;
    [HideInInspector] public string fileName;
    [HideInInspector] public int totalSqft;
    [HideInInspector] public DateTime timeStamp;
    [HideInInspector] public string timeStampString;
    [HideInInspector] public TimeSpan duration;
    [HideInInspector] public string durationString;
    [HideInInspector] public static int timeStampCount = 0;


    public void AddRun(RunReport run)
    {
        reports.Add(run);
    }

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

    public void setDuration()
    {
        DateTime currentTime = DateTime.Now;
        duration = currentTime.Subtract(timeStamp);
        durationString = duration.ToString(@"dd\.hh\:mm\:ss");
    }

    public void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", Application.persistentDataPath, "json");
    }

    public void Save()
    {
    	rooms = new List<Room>();
        foreach(GameObject room in UserInputInformation.rooms)
        {
            rooms.Add(room.GetComponent<Room>());
        }
        startValues = UserInputInformation.startVals;
        stopValues = UserInputInformation.stopVals;
        setTimeStamp();
        setDuration();
        setTotalSqft();
        // make sure that the filename is valid
        // insert here:
        //<
        //             >
    	fileName = UserInputInformation.FileNameGS;
        Debug.Log(timeStamp);
        Debug.Log(duration);
        Debug.Log(JsonUtility.ToJson(this));
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(this));
    }
}
