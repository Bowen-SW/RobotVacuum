using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour
{
    public Roomba roomba;
    public Button startButton;

    private Button startBtn;

    private PathType pathType = PathType.Random;
    private float roombaSpeed = 12F; //Defualt value 12 in/sec
    private int batteryLife = 150; //Default value
    private float simSpeed = 1F;    

    void Start() {
        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartRoomba);
    }

    public void StartRoomba(){
        roomba.init(roombaSpeed, simSpeed, batteryLife, pathType);
    }

}
