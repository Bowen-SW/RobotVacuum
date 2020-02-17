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

    public Text text;

    private Button robotSpeedBtn;

    // private RobotSpeedHandler robotSpeedBtn;

    private float robotSpeed = 12F;

    private int simulationSpeed = 1;
    void Start()
    {
        //Debug.Log("GameHandler.Start");
        robotSpeedBtn = robotSpeedButton.GetComponent<Button>();
        robotSpeedBtn.onClick.AddListener(SetRobotSpeed);

        Button startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartLaunch);
    }

    void SetRobotSpeed(){
        Text txt = text.GetComponent<Text>();

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

    void StartLaunch() {
        roomba.SetVelocity(robotSpeed, simulationSpeed);
        roomba.Launch();
    }
}
