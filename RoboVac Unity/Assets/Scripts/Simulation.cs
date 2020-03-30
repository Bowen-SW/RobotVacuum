using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Simulation : MonoBehaviour
{
    public Roomba roomba;
    public Button startButton;
    public Button pauseStopButton;
    public Button simSlowButton;
    public Button simFastButton;
    public TMP_Text simSpeedText;

    private Button startBtn;
    private Button pauseStopBtn;
    private Button simSlowBtn;
    private Button simFastBtn;
    private TMP_Text simText;

    private PathType pathType = PathType.Random; //TODO Change default All
    private float roombaSpeed = 12F; //Defualt value 12 in/sec
    private int batteryLife = 150; //Default value
    private int simSpeed = 1;    
    private bool isPaused;

    void Start() {
        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartRoomba);

        pauseStopBtn = pauseStopButton.GetComponent<Button>();
        pauseStopBtn.onClick.AddListener(PauseStopRoomba);
        pauseStopBtn.interactable = false;

        simSlowBtn = simSlowButton.GetComponent<Button>();
        simSlowBtn.onClick.AddListener(SlowDown);
        simSlowBtn.interactable = false;

        simFastBtn = simFastButton.GetComponent<Button>();
        simFastBtn.onClick.AddListener(SpeedUp);
        simFastBtn.interactable = false;
        
        simText = simSpeedText.GetComponent<TMP_Text>();
    }

    void StartRoomba(){
        startBtn.interactable = false;
        simFastBtn.interactable = true;
        pauseStopBtn.interactable = true;

        if(isPaused){   //Roomba is paused and needs to be resumed
            roomba.Resume();
            isPaused = false;
        } else {        //Roomba started for the first time  
            if(pathType == PathType.All){
                StartAllPaths();
            } else {
                roomba.init(roombaSpeed, simSpeed, batteryLife, pathType);
            }
        }
    }

    void PauseStopRoomba(){
        if(isPaused) {  //Roomba is already paused, and the stop button is hit
            roomba.Stop();
            isPaused = false;
            SetDefaults();
            //TODO change to the pause icon
        } else{         //Roomba is being paused
            roomba.Pause();
            isPaused = true;
            //TODO change to the stop icon
        }
        startBtn.interactable = true;
    }

    void SlowDown(){       
        if(simSpeed == 1){
            //Do nothing, min speed reached
        } else if(simSpeed == 25) {
            simSpeed = 1;
            simText.text = "1x";
            simSlowBtn.interactable = false;
        } else if(simSpeed == 50) {
            simSpeed = 25;
            simText.text = "25x";
            simFastBtn.interactable = true;
        } else {
            Debug.Log("Sim speed error. Reset to 1x speed.");
            simSpeed = 1;
            simText.text = "1x";
            simFastBtn.interactable = true;
        }

        roomba.SetSimSpeed(simSpeed);
    }

    void SpeedUp(){
        if(simSpeed == 1){
            simSpeed = 25;
            simText.text = "25x";
            simSlowBtn.interactable = true;
        } else if(simSpeed == 25) {
            simSpeed = 50;
            simText.text = "50x";
            simFastBtn.interactable = false;
        } else if(simSpeed == 50) {
            //Do nothing, max speed reached
        } else {
            Debug.Log("Sim speed error. Reset to 1x speed.");
            simSpeed = 1;
            simText.text = "1x";
            simSlowBtn.interactable = false;
        }

        roomba.SetSimSpeed(simSpeed);
    }

    void SetDefaults(){
        pathType = PathType.Random; //TODO Change default All
        roombaSpeed = 12F; //Defualt value 12 in/sec
        batteryLife = 150; //Default value
        simSpeed = 1;
        simText.text = "1x";
        roomba.ResetRunTime();
        pauseStopBtn.interactable = false;
    }

    void StartAllPaths(){
        //TODO
        Debug.Log("All paths chosen");
    }

}
