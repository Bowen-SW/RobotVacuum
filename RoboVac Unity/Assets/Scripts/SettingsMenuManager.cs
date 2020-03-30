using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject menu;
    public RoombaSettingsScript roombaSettings;

    // Start is called before the first frame update
    void Start()
    {
        roombaSettings = GetComponentInParent<RoombaSettingsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRoombaSpeed() //Called onChange for roomba speed slider
    {
        //int roombaSpeed = //value from speed slider
        //roombaSettings.SetRoombaSpeed(roombaSpeed);
    }

    void SetBatteryLife() //Called onChange for battery life slider
    {
        // int batteryLife = //value from the slider
        // roombaSettings.SetBatteryLife(batteryLife);
    }

    void SetPathType()
    {
        // String pathType = //string value from the dropdown menu
        // roombaSettings.SetPathType(pathType);
    }

    void SetVacuumEfficiency()
    {
        // int eff = //value from slider
        // roombaSettings.SetVacuumEfficiency(eff);
    }

    void SetWhiskerEfficiency()
    {
        // int eff = //value from slider
        // roombaSettings.SetWhiskerEfficiency(eff);   
    }

    void Accept() //Call this when the accept button is
    {
        roombaSettings.initRoomba();
    }

    public void Close()
    {
        Destroy(menu);
    }
}
