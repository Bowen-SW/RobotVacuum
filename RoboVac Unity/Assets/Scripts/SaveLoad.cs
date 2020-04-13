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
    [HideInInspector] public List<GameObject> tables = UserInputInformation.tables;
    [HideInInspector] public List<GameObject> doors = UserInputInformation.doors;
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
    [HideInInspector] private static JsonData saveData;
    public GameObject room;
    [HideInInspector] public static GameObject chair;
    [HideInInspector] public static GameObject chest;
    [HideInInspector] public static GameObject table;
    [HideInInspector] public static GameObject door;
    [HideInInspector] public static int stopCount = 0;
    //public GameObject roomba;
    // public Button button;
    void start()
    {

    }

    void Update()
    {
       if(UserInputInformation.roombaStopGS == true || (stopCount % 2 == 0 && stopCount != 0))
       {
            recordRun();
            Save();
            UserInputInformation.roombaStopGS = false;
            stopCount = 0;
       }
    }

    public void pause()
    {
        stopCount = 1;
    }
    public void setStopCount()
    {   
        stopCount++;
        Debug.Log(stopCount);
    }

    public void setCoverage()
    {   
        float _coverage = 0;
        int _sqft = 0;
        foreach(GameObject room in UserInputInformation.rooms)
        {
            Room _room = room.GetComponent<Room>(); 
            _coverage += _room.getCoverage();
            _sqft += _room.sqft;
        }
        coverage = (int)(_coverage/(float)_sqft * 100.0f);
    }

    public void setDuration()
    {
        duration = UserInputInformation.durationGS;
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
        timeStampString = UserInputInformation.timeStampString;
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
                UserInputInformation.FileNameGS = fileName;
            }        
        }
        else
        {
            fileName = UserInputInformation.FileNameGS;
        }
    }

    public void recordRun()
    {
        if(reports.Count < 1)
        {
            floorPlanIDGS = 1;
        }
        setTimeStamp();
        setCoverage();
        carpetType = UserInputInformation.carpetType;
        duration = UserInputInformation.durationGS;
        pathType = UserInputInformation.pathTypeGS;
        RunReport newRun = new RunReport(floorPlanIDGS, timeStampString, duration, pathType, coverage);
        Debug.Log(newRun);
        reports.Add(newRun);
        floorPlanIDGS++;
        Debug.Log(floorPlanIDGS);
    }

    public void setFurniture()
    {
        startValues = new List<Vector2>();
        stopValues = new List<Vector2>();
        rooms = new List<GameObject>();
        foreach(GameObject room in UserInputInformation.rooms)
        {
            rooms.Add(room);
            startValues.Add(room.GetComponent<Room>().start);
            stopValues.Add(room.GetComponent<Room>().stop);

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
        UserInputInformation.rooms.Clear();
        UserInputInformation.RoomIDNum = 0;
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
            room.GetComponent<Room>().LoadPositions(start, stop);
            UserInputInformation.AddRoom(room);
        }
    }

    public void loadDoors(JsonData saveInformation)
    {
        Debug.Log("coming soon");
    }

    public void loadTables(JsonData saveInformation)
    {
        Debug.Log("coming soon");
    }

    public void loadChests(JsonData saveInformation)
    {
        Debug.Log("coming soon");
    }

    public void loadChairs(JsonData saveInformation)
    {
        Debug.Log("coming soon");
    }

    public void loadReports(JsonData saveInformation)
    {
        if(saveInformation["reports"].Count != 0)
        {
            
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
        }
    }

    public void Load()
    {
        string fileToLoad = Loader.openFileFromBrowser();
        // if the string is empty, this indicates that the user hit cancel
        if(fileToLoad == "")
        {
            Debug.Log("Load cancelled.");
        }
        else
        {
            string jsonString = File.ReadAllText(fileToLoad);
            saveData = JsonMapper.ToObject(jsonString);

            //if this returns true, all keys necessary for loading were present
            if(Loader.validateFileFields(saveData) == true)
            {
                fileName = (string)saveData["fileName"];
                UserInputInformation.saveDataGS = saveData;
                UserInputInformation.FileNameGS = fileName;
                reports.Clear();
                removeFurniture();
                loadReports(saveData);
                loadRooms(saveData);
                loadChairs(saveData);
                loadChests(saveData);
                loadTables(saveData);
                loadDoors(saveData);
            }
        }
    }

    public void Save()
    {
        setFileName();
        setTimeStamp();
        setTotalSqft();
        setFurniture();
        setCoverage();
        setDuration();
        // this should only be called when the roomba is done or the user stops the simulation
        // recordRun() is in place now for testing purposes only
        //recordRun(); 
        Debug.Log(JsonUtility.ToJson(this));
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(this));
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json");
        saveData = JsonMapper.ToObject(jsonString);
        UserInputInformation.saveDataGS = saveData;
    }
}
