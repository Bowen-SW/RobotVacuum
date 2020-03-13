using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{

    //public Roomba roomba;
    //public FloorPlan/Simulation (room aggregator) floorplan;

    public List<GameObject> objects = new List<GameObject>();

    [HideInInspector] public List<Room> rooms = new List<Room>();
    [HideInInspector] public List<Chest> chests;
    [HideInInspector] public List<Chest> chairs;

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

    public void Save()
    {
        Debug.Log(JsonUtility.ToJson(this));
    }

}
