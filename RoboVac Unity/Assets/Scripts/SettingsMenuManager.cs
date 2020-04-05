using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject menu;
    public RoombaSettingsScript roombaSettings;

    public Slider vacuumEffSlider;
    public Slider whiskerEffSlider;
    public Slider batteryLifeSlider;
    public Slider speedSlider;
    public TextMeshProUGUI pathType;

    // Start is called before the first frame update
    void Start()
    {
        roombaSettings = GetComponentInParent<RoombaSettingsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoombaSpeed() //Called onChange for roomba speed slider
    {
        roombaSettings.SetRoombaSpeed((int)speedSlider.value);
    }

    public void SetBatteryLife() //Called onChange for battery life slider
    {
        roombaSettings.SetBatteryLife((int)batteryLifeSlider.value);
    }

    public void SetPathType()
    {
        roombaSettings.SetPathType(pathType.text);
    }

    public void SetVacuumEfficiency()
    {
        roombaSettings.SetVacuumEfficiency((int)vacuumEffSlider.value);
    }

    public void SetWhiskerEfficiency()
    {
        roombaSettings.SetWhiskerEfficiency((int)whiskerEffSlider.value);
    }

    public void Accept() //Call this when the accept button is
    {
        SetRoombaSpeed();
        SetBatteryLife();
        SetPathType();
        SetVacuumEfficiency();
        SetWhiskerEfficiency();

        Close();
    }

    public void Close()
    {
        Destroy(menu);
    }
}
