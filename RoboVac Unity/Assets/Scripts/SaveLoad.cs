using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{

    //public Roomba roomba;
    //public FloorPlan/Simulation (room aggregator) floorplan;

    [HideInInspector] public List<Room> rooms = new List<Room>();
    [HideInInspector] public List<Chest> chests;
    [HideInInspector] public List<Chest> chairs;
    [HideInInspector] public List<RunReport> reports = new List<RunReport>();
    

}

    public bool AddRoom(GameObject room)
    {
        if(room.GetComponent<Room>() != null)
        {
            rooms.Add(room.GetComponent<Room>());
            return true;
        }
        return false;
    }

    public bool AddChest(GameObject chest)
    {
        if(chest.GetComponent<Chest>() != null)
        {
            chests.Add(chest.GetComponent<Chest>());
            return true;
        }
        return false;
    }

    public bool AddChair(GameObject chair)
    {
        if(chair.GetComponent<Chest>() != null)
        {
            chairs.Add(chair.GetComponent<Chest>());
            return true;
        }
        return false;
    }

    public void AddRun(RunReport run)
    {
        reports.add(run);
    }

    public void Save()
    {
        Debug.Log(JsonUtility.ToJson(this));
    }

}
