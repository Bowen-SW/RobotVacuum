using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UserInputInformation
{
	private static string fileName = "samp";
    private static int width;
    private static int height;
    private static int RoomIDNum = 0;
    private static int ChestIDNum = 0;
    private static int ChairIDNum = 0;
    private static int TableIDNum = 0;
    public static List<GameObject> rooms = new List<GameObject>();
    public static List<Chest> chairs = new List<Chest>();
    public static List<Chest> chests = new List<Chest>();
    public static List<Vector2> startVals = new List<Vector2>();
    public static List<Vector2> stopVals = new List<Vector2>();
    public static string pathTypeUsed;
    public static string carpetType;
    public static string duration;

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
            Debug.Log("start count before " + startVals.Count);
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
            chests.Add(chest.GetComponent<Chest>());
            return true;
        }
        return false;
    }

    public static bool AddChair(GameObject chair)
    {
        if(chair.GetComponent<Chest>() != null)
        {
            chairs.Add(chair.GetComponent<Chest>());
            return true;
        }
        return false;
    }

}
