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
    private static int RoomIDNum = 0;
    // private static int ChestIDNum = 0;
    // private static int ChairIDNum = 0;
    // private static int TableIDNum = 0;
    public static List<GameObject> rooms = new List<GameObject>();
    public static List<GameObject> chairs = new List<GameObject>();
    public static List<GameObject> chests = new List<GameObject>();
    public static List<Vector2> startVals = new List<Vector2>();
    public static List<Vector2> stopVals = new List<Vector2>();
    public static string pathTypeUsed;
    public static string carpetType;
    public static string duration;
    public static DateTime timeStamp;
    public static string timeStampString;
    public static JsonData saveData;
    public static List<RunReport> prevReports;

    public static bool RoombaStop;

    public static int fileOverwriteWarning;

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

    public static string FileNameGS
    {
        get 
        {
            return fileName;
        }
        set 
        {
            fileName = value;
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
        if(chest.GetComponent<Chest>() != null)
        {
            chests.Add(chest);
            return true;
        }
        return false;
    }

    public static bool AddChair(GameObject chair)
    {
        if(chair.GetComponent<Chest>() != null)
        {
            chairs.Add(chair);
            return true;
        }
        return false;
    }
}
