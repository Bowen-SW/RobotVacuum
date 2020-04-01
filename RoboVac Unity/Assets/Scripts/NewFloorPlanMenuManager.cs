using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class NewFloorPlanMenuManager : MonoBehaviour
{
    public GameObject menu;

    public TextMeshProUGUI fileName;
    public TextMeshProUGUI roomWidth;
    public TextMeshProUGUI roomHeight;
    public TextMeshProUGUI carpetType;

    public TestSimScript simScript;

    // Start is called before the first frame update
    void Start()
    {
        simScript = GetComponentInParent<TestSimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData()
    {

    }

    public void Close()
    {
        roomHeight.text = Regex.Replace(roomHeight.text, "[^.0-9]", "");
        roomWidth.text = Regex.Replace(roomWidth.text, "[^.0-9]", "");
        UserInputInformation.FileNameGS = fileName.text;
        //check if width and height are numbers
        //check if the numbers are of the appropriate range
        UserInputInformation.RoomHeightGS = int.Parse(roomHeight.text);
        UserInputInformation.RoomWidthGS = int.Parse(roomWidth.text);

        Destroy(menu);
    }
}
