using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FurnitureWindowManager : MonoBehaviour
{
    public GameObject menu;
    public RoombaSettingsScript roombaSettings;

    public TMP_Dropdown floorDropdown;
    public TextMeshProUGUI floorType;

    // Start is called before the first frame update
    void Start()
    {
        roombaSettings = GetComponentInParent<RoombaSettingsScript>();

        PullFromSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PullFromSettings()
    {
        FloorType floor = roombaSettings.GetFloorType();

        SetFloorTypeDropdownVal(floor);
    }

    public void SetFloorTypeDropdownVal(FloorType floor)
    {
        if (floor.Equals(FloorType.Hardwood))
        {
            floorDropdown.value = 0;
        }
        else if (floor.Equals(FloorType.LoopPile))
        {
            floorDropdown.value = 1;
        }
        else if (floor.Equals(FloorType.CutPile))
        {
            floorDropdown.value = 2;
        }
        else if (floor.Equals(FloorType.FreezeCutPile))
        {
            floorDropdown.value = 3;
        }
    }

    public void UpdateSettingsFloorType()
    {
        roombaSettings.SetFloorTypeNoNotify(floorType.text);
    }

}
