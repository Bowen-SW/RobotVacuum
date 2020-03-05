using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Roomba roomba;

    public Button roombaSpeedButton;

    public Button startButton;

    public Button pathTypeButton;

    public Button pauseButton;

    public Text roombaText;

    public Text pathText;

    private Button roombaSpeedBtn;

    private Button pathTypeBtn;

    private Button startBtn;

    private Button pauseBtn;
    public Text pauseText;

    private float roombaSpeed = 12F;
    private PathType pathType;
    void Start()
    {
        roombaSpeedBtn = roombaSpeedButton.GetComponent<Button>();
        roombaSpeedBtn.onClick.AddListener(SetRoombaSpeed);

        pathTypeBtn = pathTypeButton.GetComponent<Button>();
        pathTypeBtn.onClick.AddListener(SetPathType);

        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartLaunch);
        
        pauseBtn = pauseButton.GetComponent<Button>();
        pauseBtn.onClick.AddListener(PauseAndResume);
    }

    void SetRoombaSpeed(){
        Text txt = roombaText.GetComponent<Text>();

        if(String.Equals(txt.text, "12 in/sec")){
            Debug.Log("Click to 18 in/sec");
            txt.text = "18 in/sec";
            roombaSpeed = 18F;
        } else if (String.Equals(txt.text, "18 in/sec")) {
            Debug.Log("Click to 6 in/sec");
            txt.text = "6 in/sec";
            roombaSpeed = 6F;
        } else if (String.Equals(txt.text, "6 in/sec")) {
            Debug.Log("Click to 12 in/sec");
            txt.text = "12 in/sec";
            roombaSpeed = 12F;
        } else {
            Debug.Log("Error: Unexpected value = " + txt.text + ". Setting to default");
            txt.text = "12 in/sec";
            roombaSpeed = 12F;
        }

    }

    void SetPathType(){
        Text txt = pathText.GetComponent<Text>();

        if(String.Equals(txt.text, "Random")){
            txt.text = "Snaking";
            pathType = PathType.Snaking;
            Debug.Log("Snaking");
        } else if (String.Equals(txt.text, "Snaking")) {
            txt.text = "Spiral";
            pathType = PathType.Spiral;
        } else if (String.Equals(txt.text, "Spiral")) {
            txt.text = "Wall Follow";
            pathType = PathType.WallFollow;
        } else if (String.Equals(txt.text, "Wall Follow")){
            txt.text = "All";
            pathType = PathType.All;
        } else if (String.Equals(txt.text, "All")){
            txt.text = "Random";
            pathType = PathType.Random;
        } else {
            txt.text = "Random";
            pathType = PathType.Random;
        }
    }

    void StartLaunch() {
        roombaSpeedBtn.interactable = false;
        pathTypeBtn.interactable = false;
        startBtn.interactable = false;
        pauseBtn.interactable = true;
        roomba.SetVelocities(roombaSpeed);
        roomba.SetPathType(pathType);
        roomba.Launch(); //change to roomba.path.Launch();
    }

    void PauseAndResume(){
        Text txt = pauseText.GetComponent<Text>();

        if(String.Equals(txt.text, "Pause")){
            roomba.Pause();
            txt.text = "Resume";
        } else if (String.Equals(txt.text, "Resume")){
            roomba.Resume();
            txt.text = "Pause";
        }
        
    }
}
