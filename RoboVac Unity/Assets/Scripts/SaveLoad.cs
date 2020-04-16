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
    [HideInInspector] public List<GameObject> chests;
    [HideInInspector] public List<GameObject> chairs;
    [HideInInspector] public List<GameObject> doors;
    [HideInInspector] public List<RunReport> reports = new List<RunReport>();
    [HideInInspector] public List<Vector2> startValues;
    [HideInInspector] public List<Vector2> stopValues;
    [HideInInspector] public List<Vector2> startValuesCR;
    [HideInInspector] public List<Vector2> stopValuesCR;
    [HideInInspector] public List<Vector2> startValuesCT;
    [HideInInspector] public List<Vector2> stopValuesCT;
    [HideInInspector] public List<Vector2> startValuesD;
    [HideInInspector] public List<bool> rotationD;
    [HideInInspector] public string fileName;
    [HideInInspector] public static string filePath ="";
    [HideInInspector] public static string defaultDirectory =  @"C:\RobotVacuumFiles";
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
    public GameObject chair;
    public GameObject chest;
    public GameObject door;
    public GameObject messageBox;
    public Canvas canvas;
    [HideInInspector] public static int stopCount = 0;
    //public GameObject roomba;
    // public Button button;
    void start()
    {

    }

    void Update()
    {
        //setTotalSqft();
        UserInputInformation.setTotalSqft();
        //UserInputInformation.sqftGS = totalSqft;
        //totalSqft = UserInputInformation.totalSqft;
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
        int _area = 0;
        foreach(GameObject room in UserInputInformation.rooms)
        {
            Room _room = room.GetComponent<Room>(); 
            _coverage += _room.getCoverage();
            _sqft += _room.sqft;
        }

        foreach(GameObject chest in UserInputInformation.chests)
        {
            Chest _chest = chest.GetComponentInChildren<Chest>();
            _area += _chest.area;
        }
        coverage = (int)(_coverage/(float)(_sqft - _area) * 100.0f);
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
            while(File.Exists(@"C:\RobotVacuumFiles" + "/" + fileName + ".json"))
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
        startValuesCR = new List<Vector2>();
        stopValuesCR = new List<Vector2>();
        startValuesCT = new List<Vector2>();
        stopValuesCT = new List<Vector2>();
        startValuesD = new List<Vector2>();
        rotationD = new List<bool>();

        rooms = new List<GameObject>();
        foreach(GameObject room in UserInputInformation.rooms)
        {
            rooms.Add(room);
            startValues.Add(room.GetComponent<Room>().start);
            stopValues.Add(room.GetComponent<Room>().stop);

        }
        chests = new List<GameObject>();
        foreach(GameObject chest in UserInputInformation.chests)
        {
            chests.Add(chest);
            startValuesCT.Add(chest.GetComponentInChildren<Chest>().start);
            stopValuesCT.Add(chest.GetComponentInChildren<Chest>().stop);
        }
        chairs = new List<GameObject>();
        foreach(GameObject chair in UserInputInformation.chairs)
        {
            chairs.Add(chair);
            startValuesCR.Add(chair.GetComponentInChildren<Chair>().start);
            stopValuesCR.Add(chair.GetComponentInChildren<Chair>().stop);
        }
        doors = new List<GameObject>();
        foreach(GameObject door in UserInputInformation.doors)
        {
            doors.Add(door);
            startValuesD.Add(door.GetComponent<Door>().getXYPos());
            rotationD.Add(door.GetComponent<Door>().isRotated);
        }
    }

    public void removeFurniture()
    {
        foreach(GameObject room in UserInputInformation.rooms)
        {
            Destroy(room);
        }
        UserInputInformation.rooms.Clear();
        foreach(GameObject chest in UserInputInformation.chests)
        {
            Destroy(chest);
        }
        UserInputInformation.chests.Clear();
        foreach(GameObject chair in UserInputInformation.chairs)
        {
            Destroy(chair);
        }
        UserInputInformation.chairs.Clear();
        foreach(GameObject door in UserInputInformation.doors)
        {
            Destroy(door);
        }
        UserInputInformation.doors.Clear();
        UserInputInformation.resetVectorLists();
    }

    public void loadRooms(JsonData saveInformation)
    {
        UserInputInformation.stopVals.Clear();
        UserInputInformation.startVals.Clear();
        UserInputInformation.rooms.Clear();
        UserInputInformation.RoomIDNum = 0;
        Debug.Log("count: " + saveInformation["rooms"].Count);
        for(int i = 0; i < saveInformation["rooms"].Count; i++)
        {
            double stopX = (double)saveData["stopValues"][i]["x"];
            double stopY = (double)saveData["stopValues"][i]["y"];

            double startX = (double)saveData["startValues"][i]["x"];
            double startY = (double)saveData["startValues"][i]["y"];

            Vector2 start = new Vector2((float)startX, (float)startY);
            Vector2 stop = new Vector2((float)stopX, (float)stopY);
            Debug.Log(start + ";" + stop);

            GameObject new_room = (GameObject)Instantiate(room, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            new_room.GetComponent<Room>().LoadPositions(start, stop);
            UserInputInformation.AddRoom(new_room);
        }
    }

    public void loadDoors(JsonData saveInformation)
    {
        UserInputInformation.startValsD.Clear();
        UserInputInformation.doors.Clear();
        UserInputInformation.DoorIDNum = 0;
        Debug.Log("count: " + saveInformation["doors"].Count);
        for(int i = 0; i < saveInformation["doors"].Count; i++)
        {
            double startX = (double)saveData["startValuesD"][i]["x"];
            double startY = (double)saveData["startValuesD"][i]["y"];

            bool isRotated = (bool)saveData["rotationD"][i];

            Vector2 start = new Vector2((float)startX, (float)startY);
            Debug.Log(start);

            GameObject new_door = (GameObject)Instantiate(door, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            new_door.GetComponent<Door>().LoadPositions(start, isRotated);
            UserInputInformation.AddDoor(new_door);
        }
    }

    public void loadChests(JsonData saveInformation)
    {
        UserInputInformation.stopValsCT.Clear();
        UserInputInformation.startValsCT.Clear();
        UserInputInformation.chests.Clear();
        UserInputInformation.ChestIDNum = 0;
        Debug.Log("count: " + saveInformation["chests"].Count);
        for(int i = 0; i < saveInformation["chests"].Count; i++)
        {
            double stopX = (double)saveData["stopValuesCT"][i]["x"];
            double stopY = (double)saveData["stopValuesCT"][i]["y"];

            double startX = (double)saveData["startValuesCT"][i]["x"];
            double startY = (double)saveData["startValuesCT"][i]["y"];

            Vector2 start = new Vector2((float)startX, (float)startY);
            Vector2 stop = new Vector2((float)stopX, (float)stopY);
            Debug.Log(start + ";" + stop);

            GameObject new_chest = (GameObject)Instantiate(chest, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            new_chest.GetComponentInChildren<Chest>().LoadPositions(start, stop);
            UserInputInformation.AddChest(new_chest);
        }
    }

    public void loadChairs(JsonData saveInformation)
    {
        UserInputInformation.stopValsCR.Clear();
        UserInputInformation.startValsCR.Clear();
        UserInputInformation.chairs.Clear();
        UserInputInformation.ChairIDNum = 0;
        Debug.Log("count: " + saveInformation["chairs"].Count);
        for(int i = 0; i < saveInformation["chairs"].Count; i++)
        {
            double stopX = (double)saveData["stopValuesCR"][i]["x"];
            double stopY = (double)saveData["stopValuesCR"][i]["y"];

            double startX = (double)saveData["startValuesCR"][i]["x"];
            double startY = (double)saveData["startValuesCR"][i]["y"];

            Vector2 start = new Vector2((float)startX, (float)startY);
            Vector2 stop = new Vector2((float)stopX, (float)stopY);
            Debug.Log(start + ";" + stop);

            GameObject new_chair = (GameObject)Instantiate(chair, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            new_chair.GetComponentInChildren<Chair>().LoadPositions(start, stop);
            UserInputInformation.AddChair(new_chair);
        }
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
        }
    }

    public void Load()
    {
        string fileToLoad = Loader.openFileFromBrowser();
        try
        {
            filePath = fileToLoad;
            string jsonString = File.ReadAllText(fileToLoad);
            saveData = JsonMapper.ToObject(jsonString);

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
                loadDoors(saveData);
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("MessageBox") == null)
                {
                    GameObject newObj = Instantiate(messageBox, new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    newObj.transform.SetParent(canvas.transform, false);
                    RectTransform objRectTrans = newObj.GetComponent<RectTransform>();
                    objRectTrans.offsetMin = new Vector2(0.0f, 0.0f);
                    objRectTrans.offsetMax = new Vector2(0.0f, 0.0f);
                }
            
            }
        }
        catch
        {
            Debug.Log("cancel");
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
        Debug.Log(filePath);
        Debug.Log(JsonUtility.ToJson(this));
        
        if(filePath == "")
        {
            try 
            {
                if (Directory.Exists(defaultDirectory)) 
                {
                    filePath = (defaultDirectory + "\\" + fileName + ".json");
                    Debug.Log(filePath);
                    File.WriteAllText(filePath, JsonUtility.ToJson(this));
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(defaultDirectory);
                filePath = (defaultDirectory + "\\" + fileName + ".json");
                Debug.Log(filePath);
                File.WriteAllText(filePath, JsonUtility.ToJson(this));
            } 
            catch (Exception e) 
            {
                Debug.Log(e.ToString());
            }
                            

        }
        else
        {
            filePath = (defaultDirectory + "\\" + fileName + ".json");
            Debug.Log(filePath);
            File.WriteAllText(filePath, JsonUtility.ToJson(this));
        }

        string jsonString = File.ReadAllText(filePath);
        Debug.Log(jsonString);
        saveData = JsonMapper.ToObject(jsonString);
        UserInputInformation.saveDataGS = saveData;
    }
}
