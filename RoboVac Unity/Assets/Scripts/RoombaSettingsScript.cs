using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaSettingsScript : MonoBehaviour
{
    
    public Roomba roomba;
    public FurnitureWindowManager furnitureWindowManager;

    private int roombaSpeed = 12;
    private int batteryLife = 150;
    private float vacEff = 90F;
    private float whiskerEff = 30F;
    private PathType pathType = PathType.All;
    private FloorType floorType = FloorType.Hardwood;

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

    public FloorType GetFloorType(){
        return floorType;
    }

    public void SetFloorType(String floor){
        if (String.Equals(floor, "Cut Pile")){
            floorType = FloorType.CutPile;
        } else if (String.Equals(floor, "Loop Pile")){
            floorType = FloorType.LoopPile;
        } else if (String.Equals(floor, "Hardwood")){
            floorType = FloorType.Hardwood;
        } else if (String.Equals(floor, "Frieze-Cut Pile")){
            floorType = FloorType.FreezeCutPile;
        }

        furnitureWindowManager.SetFloorTypeDropdownVal(floorType);

    }

    public void SetFloorTypeNoNotify(String floor)
    {
        if (String.Equals(floor, "Cut Pile"))
        {
            floorType = FloorType.CutPile;
        }
        else if (String.Equals(floor, "Loop Pile"))
        {
            floorType = FloorType.LoopPile;
        }
        else if (String.Equals(floor, "Hardwood"))
        {
            floorType = FloorType.Hardwood;
        }
        else if (String.Equals(floor, "Frieze-Cut Pile"))
        {
            floorType = FloorType.FreezeCutPile;
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
