using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaSettingsScript : MonoBehaviour
{
    
    public Roomba roomba;

    private int roombaSpeed = 12;
    private int batteryLife = 150;
    private float vacEff = 75F;
    private float whiskerEff = 30F;
    private PathType pathType = PathType.All;

    public int GetRoombaSpeed(){
        return roombaSpeed;
    }
    public void SetRoombaSpeed(int roombaSpeed){
        this.roombaSpeed = roombaSpeed;
    }

    public int GetBatteryLife(){
        return batteryLife;
    }
    public void SetBatteryLife(int batteryLife){
        this.batteryLife = batteryLife;
    }

    public PathType GetPathType(){
        return pathType;
    }
    public void SetPathType(String path){
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

    public float GetVacuumEfficiency(){
        return vacEff;
    }
    public void SetVacuumEfficiency(float vacEff){
        this.vacEff = vacEff;
    }
 
    public float GetWhiskerEfficiency(){
        return whiskerEff;
    }
    public void SetWhiskerEfficiency(float whiskerEff){
        this.whiskerEff = whiskerEff;
    }
}
