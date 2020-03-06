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
    public Slider roombaSpeedSlider;
    public Text speedText;
    public Text pauseText;

    private Button roombaSpeedBtn;
    private Button pathTypeBtn;
    private Button startBtn;
    private Button pauseBtn;
    private float roombaSpeed = 12F; //Defualt value 12 in/sec
    private PathType pathType;

    void Start()
    {
        roombaSpeedBtn = roombaSpeedButton.GetComponent<Button>();
        roombaSpeedBtn.onClick.AddListener(SetRoombaSpeed);

        pathTypeBtn = pathTypeButton.GetComponent<Button>();
        pathTypeButton.onClick.AddListener(SetPathType);

        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartRoomba);
        
        pauseBtn = pauseButton.GetComponent<Button>();
        pauseBtn.onClick.AddListener(PauseAndResume);

        roombaSpeedSlider.onValueChanged.AddListener(delegate {SetRoombaSpeed();});
    }

    void SetRoombaSpeed(){
        Text txt = speedText.GetComponent<Text>();

        //Debug.Log("Slider value = " + roombaSpeedSlider.value.ToString());
        txt.text = roombaSpeedSlider.value.ToString();
        roombaSpeed = roombaSpeedSlider.value;
    }

    void SetPathType(){
        Text txt = pathText.GetComponent<Text>();

        if(String.Equals(txt.text, "Random")){
            txt.text = "Snaking";
            pathType = PathType.Snaking;
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

    void StartRoomba() {
        roombaSpeedBtn.interactable = false;
        pathTypeBtn.interactable = false;
        startBtn.interactable = false;
        pauseBtn.interactable = true;
        roomba.init(roombaSpeed, pathType);
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
