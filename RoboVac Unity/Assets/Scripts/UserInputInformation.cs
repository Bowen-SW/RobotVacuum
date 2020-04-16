using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;

public static class UserInputInformation
{
	private static string fileName = "";
    private static int width;
    private static int height;
    public static int RoomIDNum = 0;
    public static int ChestIDNum = 0;
    public static int ChairIDNum = 0;
    public static int DoorIDNum = 0;
    public static int totalSqft = 0;
    public static List<GameObject> rooms = new List<GameObject>();
    public static List<GameObject> chairs = new List<GameObject>();
    public static List<GameObject> chests = new List<GameObject>();
    public static List<GameObject> doors = new List<GameObject>();
    public static List<Vector2> startVals = new List<Vector2>();
    public static List<Vector2> stopVals = new List<Vector2>();
    public static List<Vector2> startValsD = new List<Vector2>();
    public static List<bool> rotationD = new List<bool>();
    public static List<Vector2> startValsCR = new List<Vector2>();
    public static List<Vector2> stopValsCR = new List<Vector2>();
    public static List<Vector2> startValsCT = new List<Vector2>();
    public static List<Vector2> stopValsCT = new List<Vector2>();
    public static string pathTypeUsed;
    public static string carpetType;
    public static string duration;
    public static DateTime timeStamp;
    public static string timeStampString;
    public static JsonData saveData;
    public static List<RunReport> prevReports;

    public static bool RoombaStop;

    public static int fileOverwriteWarning;

    public static void resetIDNums()
    {
        RoomIDNum = 0;
        ChestIDNum = 0;
        ChairIDNum = 0;
        DoorIDNum = 0;
    }

    public static void resetVectorLists()
    {
        startVals.Clear();
        stopVals.Clear();

        startValsCR.Clear();
        stopValsCR.Clear();

        startValsCT.Clear();
        stopValsCT.Clear();

        startValsD.Clear();
        rotationD.Clear();
    }

    public static void setTotalSqft()
    {
        totalSqft = 0;
        foreach(GameObject room in rooms)
        {
            int width = ((int)(room.GetComponent<Room>().stop.x - room.GetComponent<Room>().start.x));
            int height = ((int)(room.GetComponent<Room>().stop.y - room.GetComponent<Room>().start.y));
            totalSqft += (width*height);
            //Debug.Log("Updating sqft: " + totalSqft);
        }
    }
    
    public static string setstartTime()
    {
        timeStamp = DateTime.Now;
        timeStampString = timeStamp.ToString("MM-dd-yyyy @ HH:mm", DateTimeFormatInfo.InvariantInfo);
        return timeStampString;
    }

    public static JsonData saveDataGS
    {
        get
        {
            return saveData;
        }
        set
        {
            saveData = value;
        }
    }

    public static int sqftGS
    {
        get
        {
            return totalSqft;
        }
        set
        {
            totalSqft = value;
        }
    }

    public static int overwriteWarningGS
    {
        get
        {
            return fileOverwriteWarning;
        }
        set
        {
            fileOverwriteWarning = value;
        }
    }

    public static bool roombaStopGS
    {
        get
        {
            return RoombaStop;
        }
        set
        {
            RoombaStop = value;
        }
    }

    public static string pathTypeGS
    {
        get
        {
            return pathTypeUsed;
        }
        set
        {
            pathTypeUsed = value;
        }
    }

    public static string carpetTypeGS
    {
        get
        {
            return carpetType;
        }
        set
        {
            carpetType = value;
        }
    }

    public static string durationGS
    {
        get
        {
            return duration;
        }
        set
        {
            duration = value;
        }
    }

    public static void AddStartVector(int roomID, Vector2 startVector)
    {
        if(startVals.Count == roomID)
        {
            startVals.Add(startVector);
        }
        else
        {
            startVals[roomID] = startVector;          
        }
    }

    public static void AddStopVector(int roomID, Vector2 stopVector)
    {
        if(stopVals.Count == roomID)
        {
            stopVals.Add(stopVector);
        }
        else
        {
            stopVals[roomID] = stopVector;           
        }
    }    

    public static void AddStartVectorCR(int chairID, Vector2 startVector)
    {
        if(startValsCR.Count == chairID)
        {
            startValsCR.Add(startVector);
        }
        else
        {
            startValsCR[chairID] = startVector;          
        }
    }

    public static void AddStopVectorCR(int chairID, Vector2 stopVector)
    {
        if(stopValsCR.Count == chairID)
        {
            stopValsCR.Add(stopVector);
        }
        else
        {
            stopValsCR[chairID] = stopVector;           
        }
    }  
    public static void AddStartVectorCT(int chestID, Vector2 startVector)
    {
        if(startValsCT.Count == chestID)
        {
            startValsCT.Add(startVector);
        }
        else
        {
            startValsCT[chestID] = startVector;          
        }
    }

    public static void AddStopVectorCT(int chestID, Vector2 stopVector)
    {
        if(stopValsCT.Count == chestID)
        {
            stopValsCT.Add(stopVector);
        }
        else
        {
            stopValsCT[chestID] = stopVector;           
        }
    } 

    public static void AddStartVectorD(int doorID, Vector2 targetVector)
    {
        if(startValsD.Count == doorID)
        {
            startValsD.Add(targetVector);
        }
        else
        {
            startValsD[doorID] = targetVector;          
        }
    }

    public static void AddRotationBoolD(int doorID, bool isRotated)
    {
        if(rotationD.Count == doorID)
        {
            rotationD.Add(isRotated);
        }
        else
        {
            rotationD[doorID] = isRotated;          
        }
    }

    public static string FileNameGS
    {
        get 
        {
            return fileName;
        }
        set 
        {
            fileName = value;
            Debug.Log("setting file name: " + fileName);
        }
    }

    public static int RoomWidthGS
    {
        get 
        {
            return width;
        }
        set 
        {
            width = value;
        }
    }

    public static int RoomHeightGS
    {
        get 
        {
            return height;
        }
        set 
        {
            height = value;
        }
    }

    public static bool RemoveItem(GameObject item)
    {
        if(item == null)
        {
            return false;
        }
        if (item.GetComponent<Room>() != null)
        {
            rooms.Remove(item);
            return true;
        }
        else if (item.GetComponent<Chest>() != null)
        {
            chests.Remove(item);
            return true;
        }
        else if (item.GetComponent<Chair>() != null)
        {
            chairs.Remove(item);
            return true;
        }
        else if (item.GetComponent<Door>() != null)
        {
            doors.Remove(item);
            return true;
        }

        return false;
    }

    public static bool AddRoom(GameObject room)
    {
        if(room.GetComponent<Room>() != null)
        {
            rooms.Add(room);
            room.GetComponent<Room>().id = RoomIDNum;
            RoomIDNum ++;
            return true;
        }
        return false;
    }

    public static bool AddChest(GameObject chest)
    {
        if(chest.GetComponentInChildren<Chest>() != null)
        {
            chests.Add(chest);
            chest.GetComponentInChildren<Chest>().id = ChestIDNum;
            Debug.Log(chest.GetComponentInChildren<Chest>().id);
            ChestIDNum ++;
            return true;
        }
        return false;
    }

    public static bool AddChair(GameObject chair)
    {
        if(chair.GetComponentInChildren<Chair>() != null)
        {
            chairs.Add(chair);
            chair.GetComponentInChildren<Chair>().idCR = ChairIDNum;
            ChairIDNum ++;
            return true;
        }
        return false;
    }

    public static bool AddDoor(GameObject door)
    {
        if(door.GetComponent<Door>() != null)
        {
            doors.Add(door);
            door.GetComponent<Door>().id = DoorIDNum;
            DoorIDNum ++;
            return true;
        }
        return false;
    }
}
