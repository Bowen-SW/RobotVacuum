using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaSettingsScript : MonoBehaviour
{
    
    public Roomba roomba;

    private int roombaSpeed;
    private int simSpeed = 1;
    private int batteryLife;
    private PathType pathType;


    public void SetRoombaSpeed(int roombaSpeed)
    {
        this.roombaSpeed = roombaSpeed;
    }

    public void SetBatteryLife(int batteryLife)
    {
        this.batteryLife = batteryLife;
    }

    public void SetPathType(String path)
    {
        if(String.Equals(path, "Random")){
            pathType = PathType.Random;
        } else if (String.Equals(path, "Snaking")) {
            pathType = PathType.Snaking;
        } else if (String.Equals(path, "Spiral")) {
            pathType = PathType.Spiral;
        } else if (String.Equals(path, "Wall Follow")){
            pathType = PathType.WallFollow;
        } else if (String.Equals(path, "All")){
            pathType = PathType.All;
        }
    }

    public void SetVacuumEfficiency(int vacEff)
    {
        roomba.SetVacEff(vacEff);
    }

    public void SetWhiskerEfficiency(int whiskerEff)
    {
        roomba.SetWhiskerEff(whiskerEff);
    }

    public void initRoomba()
    {
        roomba.init(roombaSpeed, simSpeed, batteryLife, pathType);
    }

    public void Pause()
    {
        roomba.Pause();
    }
}
