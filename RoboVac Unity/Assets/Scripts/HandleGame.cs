using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Roomba roomba;

    public Button robotSpeedButton;

    public Button startButton;

    public Button simSpeedButton;

    public Button pauseButton;

    public Text robotText;

    public Text simText;

    private Button robotSpeedBtn;

    private Button simSpeedBtn;

    private Button startBtn;

    private Button pauseBtn;

    private float robotSpeed = 12F;

    private int simulationSpeed = 1;
    void Start()
    {
        robotSpeedBtn = robotSpeedButton.GetComponent<Button>();
        robotSpeedBtn.onClick.AddListener(SetRobotSpeed);

        simSpeedBtn = simSpeedButton.GetComponent<Button>();
        simSpeedBtn.onClick.AddListener(SetSimSpeed);

        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartLaunch);
        
        pauseBtn = pauseButton.GetComponent<Button>();
        pauseBtn.onClick.AddListener(Pause);
    }

    void SetRobotSpeed(){
        Text txt = robotText.GetComponent<Text>();

        if(String.Equals(txt.text, "12 in/sec")){
            Debug.Log("Click to 18 in/sec");
            txt.text = "18 in/sec";
            robotSpeed = 18F;
        } else if (String.Equals(txt.text, "18 in/sec")) {
            Debug.Log("Click to 6 in/sec");
            txt.text = "6 in/sec";
            robotSpeed = 6F;
        } else if (String.Equals(txt.text, "6 in/sec")) {
            Debug.Log("Click to 12 in/sec");
            txt.text = "12 in/sec";
            robotSpeed = 12F;
        } else {
            Debug.Log("Error: Unexpected value = " + txt.text + ". Setting to default");
            txt.text = "12 in/sec";
            robotSpeed = 12F;
        }

    }

    void SetSimSpeed(){
        Text txt = simText.GetComponent<Text>();

        if(String.Equals(txt.text, "1x Speed")){
            // Debug.Log("Click to 18 in/sec");
            txt.text = "25x Speed";
            simulationSpeed = 25;
        } else if (String.Equals(txt.text, "25x Speed")) {
            // Debug.Log("Click to 6 in/sec");
            txt.text = "50x Speed";
            simulationSpeed = 50;
        } else if (String.Equals(txt.text, "50x Speed")) {
            // Debug.Log("Click to 12 in/sec");
            txt.text = "1x Speed";
            simulationSpeed = 1;
        } else {
            Debug.Log("Error: Unexpected value = " + txt.text + ". Setting to default");
            txt.text = "1x Speed";
            simulationSpeed = 1;
        }
    }

    void StartLaunch() {
        robotSpeedBtn.interactable = false;
        simSpeedBtn.interactable = false;
        startBtn.interactable = false;
        pauseBtn.interactable = true;
        roomba.SetVelocity(robotSpeed);
        roomba.Launch();
    }

    void Pause(){
        startBtn.interactable = true;
        pauseBtn.interactable = false;
        roomba.path.Stop();
    }
}
