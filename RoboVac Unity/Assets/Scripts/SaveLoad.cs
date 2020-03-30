using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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
    public string fileName;
    public int totalSqft;


    public void AddRun(RunReport run)
    {
        reports.Add(run);
    }

    public void getTotalSqft()
    {
        totalSqft = 0;
        for(int i = 0; i < startValues.Count; i++)
        {
            int width = ((int)(stopValues[i].x - startValues[i].x));
            int height = ((int)(stopValues[i].y - startValues[i].y));
            totalSqft += (width*height);
        }
    }

    public void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", Application.persistentDataPath, "json");
    }

    public void Save()
    {
    	rooms = new List<Room>();
        startValues = UserInputInformation.startVals;
        stopValues = UserInputInformation.stopVals;

        getTotalSqft();
    	fileName = UserInputInformation.FileNameGS;
        Debug.Log(JsonUtility.ToJson(this));
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(this));
    }

}
