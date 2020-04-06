using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;
using System.Globalization;
using LitJson;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    [HideInInspector] public List<GameObject> rooms;
    [HideInInspector] public List<GameObject> chests = UserInputInformation.chests;
    [HideInInspector] public List<GameObject> chairs = UserInputInformation.chairs;
    [HideInInspector] public List<RunReport> reports = new List<RunReport>();
    [HideInInspector] public List<Vector2> startValues;
    [HideInInspector] public List<Vector2> stopValues;
    [HideInInspector] public string fileName;
    [HideInInspector] public int totalSqft;
    [HideInInspector] public static DateTime timeStamp;
    [HideInInspector] public static int floorPlanID = 1;
    [HideInInspector] public static string timeStampString;
    [HideInInspector] public static string duration;
    [HideInInspector] public static string carpetType;
    [HideInInspector] public static string pathType;
    [HideInInspector] public static int timeStampCount = 0;
    [HideInInspector] public static int coverage;
    [HideInInspector] private JsonData saveData;
    [HideInInspector] public GameObject room;
    [HideInInspector] public GameObject chair;
    [HideInInspector] public GameObject chest;
    [HideInInspector] public GameObject table;
    [HideInInspector] public GameObject door;
    [HideInInspector] public int stopCount;
    // public GameObject roomba;
    // // public Button button;
    // void start()
    // {

    // }
    // public void setStopCount()
    // {   
    //     stopCount++;
    //     if(stopCount%2 == 0)
    //     {
    //         recordRun();
    //         Save();
    //     }
    // }

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
            timeStampString = timeStamp.ToString("MM-dd-yyyy @ HH:mm", DateTimeFormatInfo.InvariantInfo);
            timeStampCount++;
        }
    }

    public static int floorPlanIDGS
    {
        get
        {
            return floorPlanID;
        }
        set
        {
            floorPlanID = value;
        }
    }

    public void setFileName()
    {
        if(UserInputInformation.FileNameGS == "")
        {
            int appendedNum = 1;
            fileName = "floorPlan"+ appendedNum.ToString();
            while(File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
            {
                appendedNum++;
                fileName = "floorPlan"+ appendedNum.ToString();
            }        
        }
        else
        {
            fileName = UserInputInformation.FileNameGS;
        }
    }

    public void recordRun()
    {
        if(UserInputInformation.overwriteWarningGS == 1)
        {
            floorPlanIDGS = 1;
        }
        carpetType = UserInputInformation.carpetType;
        duration = UserInputInformation.durationGS;
        pathType = UserInputInformation.pathTypeGS;
        coverage = 0;
        RunReport newRun = new RunReport(floorPlanIDGS, timeStampString, duration, pathType, coverage);
        Debug.Log(newRun);
        reports.Add(newRun);
        floorPlanIDGS++;
        Debug.Log(floorPlanIDGS);
    }

    public void setFurniture()
    {
        rooms = new List<GameObject>();
        foreach(GameObject room in UserInputInformation.rooms)
        {
            rooms.Add(room);
        }
        // chests = new List<Room>();
        // foreach(GameObject room in UserInputInformation.rooms)
        // {
        //     rooms.Add(room.GetComponent<Room>());
        // }
        // chairs = new List<Room>();
        // foreach(GameObject room in UserInputInformation.rooms)
        // {
        //     rooms.Add(room.GetComponent<Room>());
        // }
        // tables = new List<Room>();
        // foreach(GameObject room in UserInputInformation.rooms)
        // {
        //     rooms.Add(room.GetComponent<Room>());
        // }
        startValues = UserInputInformation.startVals;
        stopValues = UserInputInformation.stopVals;
    }

    public void removeFurniture()
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

    public void loadRooms(JsonData saveInformation)
    {
        UserInputInformation.stopVals.Clear();
        UserInputInformation.startVals.Clear();
        for(int i = 0; i < saveInformation["rooms"].Count; i++)
        {
            double stopX = (double)saveData["stopValues"][i]["x"];
            double stopY = (double)saveData["stopValues"][i]["y"];

            double startX = (double)saveData["startValues"][i]["x"];
            double startY = (double)saveData["startValues"][i]["y"];

            Vector2 start = new Vector2((float)startX, (float)startY);
            Vector2 stop = new Vector2((float)stopX, (float)stopY);
            Debug.Log(start + ";" + stop);

            room = (GameObject)Instantiate(room, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            room.GetComponent<Room>().SetStart(start);
            room.GetComponent<Room>().SetStart(stop);
            UserInputInformation.AddRoom(room);
        }
    }

    public void Load()
    {
        removeFurniture();
        reports.Clear();
        string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", Application.persistentDataPath, "json");
        string jsonString = File.ReadAllText(filePath);
        saveData = JsonMapper.ToObject(jsonString);
        UserInputInformation.saveDataGS = saveData;
        fileName = (string)saveData["fileName"];
        for(int i = 0; i < saveData["reports"].Count; i++)
        {
            int id = (int)(saveData["reports"][i]["reportID"]);
            string date = (string)saveData["reports"][i]["dateStamp"];
            string duration = (string)saveData["reports"][i]["duration"];
            string alg = (string)saveData["reports"][i]["algorithmType"];
            int coverage =(int)(saveData["reports"][i]["coverageValue"]);
            floorPlanIDGS = i;
            RunReport prevRun = new RunReport(id, date, duration, alg, coverage);
            reports.Add(prevRun);
        }

        UserInputInformation.prevReports = reports;
        loadRooms(saveData);
        
        // for(int i = 0; i<reports.Count; i++)
        // {
        //     Debug.Log(reports[i].getID());
        //     Debug.Log(reports[i].getDate());
        //     Debug.Log(reports[i].getDuration());
        //     Debug.Log(reports[i].getAlgorithmType());
        //     Debug.Log(reports[i].getCoverage());
        // }   
    }

    public void Save()
    {
        setFileName();
        setTimeStamp();
        setTotalSqft();
        setFurniture();
        // this should only be called when the roomba is done or the user stops the simulation
        // recordRun() is in place now for testing purposes only
        recordRun(); 
        Debug.Log(JsonUtility.ToJson(this));
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(this));
    }
}
