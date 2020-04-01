using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Simulation : MonoBehaviour
{
    public Roomba roomba;
    public Button startStopButton;
    public Button pauseButton;
    public Button simSlowButton;
    public Button simFastButton;
    public TMP_Text simSpeedText;

    private Button startStopBtn;
    private Button pauseBtn;
    private Button simSlowBtn;
    private Button simFastBtn;
    private TMP_Text simText;

    private PathType pathType = PathType.Snaking; //TODO Change default All
    private float roombaSpeed = 12F; //Defualt value 12 in/sec
    private int batteryLife = 150; //Default value
    private int simSpeed = 1;    
    private bool isPaused = false;
    private bool isPlaying = false;
    private bool isStopped = true;

    private Queue<PathType> pathList;

    void Start() {
        startStopBtn = startStopButton.GetComponent<Button>();
        startStopBtn.onClick.AddListener(StartStopRoomba);

        pauseBtn = pauseButton.GetComponent<Button>();
        pauseBtn.onClick.AddListener(PauseRoomba);
        pauseBtn.interactable = false;

        simSlowBtn = simSlowButton.GetComponent<Button>();
        simSlowBtn.onClick.AddListener(SlowDown);
        simSlowBtn.interactable = false;

        simFastBtn = simFastButton.GetComponent<Button>();
        simFastBtn.onClick.AddListener(SpeedUp);
        simFastBtn.interactable = false;
        
        simText = simSpeedText.GetComponent<TMP_Text>();

        pathList = new Queue<PathType>();
    }

    void Update(){
        if(roomba.IsTimeLimitReached()){
            isPaused = false;
            isPlaying = false;
            isStopped = true;
            roomba.SetTimeLimitReached(false);
            SetDefaults();
            if(pathList.Count != 0){
                pathType = pathList.Peek();
                pathList.Dequeue();
                StartStopRoomba();
            }
        }
    }

    public void StartStopRoomba(){
        simFastBtn.interactable = true;
        pauseBtn.interactable = true;

        if(isPaused){   //Roomba is paused and needs to be resumed
            roomba.Resume();
            isPaused = false;
            isStopped = false;
            isPlaying = true;
            //TODO set the icon to a stop button
        } else if(isPlaying){
            roomba.Stop();
            isPaused = false;
            isPlaying = false;
            isStopped = true;
            SetDefaults();
            pathList = new Queue<PathType>(); //Reset the queue if the stop button is clicked
            //TODO Set icon to the Play icon
        } else if(isStopped) {        //Roomba started for the first time  
            if(pathType == PathType.All){
                StartAllPaths();
            }
            InitRoomba();
            //TODO set the icon to a stop button
        }
    }

    void PauseRoomba(){
        roomba.Pause();
        isPaused = true;
        isPlaying = false;
        pauseBtn.interactable = false;
        //TODO turn the stop button into a play button
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

    private void InitRoomba()
    {
        roomba.init(roombaSpeed, simSpeed, batteryLife, pathType);
        isStopped = false;
        isPlaying = true;
    }

    void SetDefaults(){
        simSpeed = 1;
        simText.text = "1x";
        roomba.ResetRunTime();
        pauseBtn.interactable = false;
        simSlowBtn.interactable = false;
        simFastBtn.interactable = false;
    }

    void StartAllPaths(){
        pathType = PathType.Random;
        pathList.Enqueue(PathType.Snaking);
        pathList.Enqueue(PathType.Spiral);
        pathList.Enqueue(PathType.WallFollow);
        Debug.Log("All paths chosen");
    }

}
