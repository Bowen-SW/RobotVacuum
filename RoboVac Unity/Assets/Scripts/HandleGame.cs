using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Roomba roomba;
    public Button simSpeedButton;
    public Button startButton;
    public Button pathTypeButton;
    public Button pauseButton;
    public Slider roombaSpeedSlider;
    public Slider batterySlider;
    public Text simText;
    public Text pathText;
    public Text speedText;
    public Text pauseText;
    public Text batteryText;

    private Button simSpeedBtn;
    private Button pathTypeBtn;
    private Button startBtn;
    private Button pauseBtn;
    private PathType pathType;
    private float roombaSpeed = 12F; //Defualt value 12 in/sec
    private float batteryLife = 150F; //Default value
    private float simSpeed = 1F;    

    void Start()
    {
        simSpeedBtn = simSpeedButton.GetComponent<Button>();
        simSpeedBtn.onClick.AddListener(SetSimSpeed);

        pathTypeBtn = pathTypeButton.GetComponent<Button>();
        pathTypeButton.onClick.AddListener(SetPathType);

        startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(StartRoomba);
        
        pauseBtn = pauseButton.GetComponent<Button>();
        pauseBtn.onClick.AddListener(PauseAndResume);

        roombaSpeedSlider.onValueChanged.AddListener(delegate {SetRoombaSpeed();});
        //TODO add battery life slider
        batterySlider.onValueChanged.AddListener(delegate {SetBatteryLife();});
    }

    void SetRoombaSpeed(){
        Text txt = speedText.GetComponent<Text>();

        //Debug.Log("Slider value = " + roombaSpeedSlider.value.ToString());
        txt.text = roombaSpeedSlider.value.ToString();
        roombaSpeed = roombaSpeedSlider.value;
    }

    void SetBatteryLife(){
        Text txt = batteryText.GetComponent<Text>();

        batteryLife = batterySlider.value;
        txt.text = batteryLife.ToString();

        Debug.Log("Slider value = " + txt.text);
    }

    void SetSimSpeed(){
        Text txt = simText.GetComponent<Text>();

        if (String.Equals(txt.text, "1x Speed")){
            txt.text = "25x Speed";
            simSpeed = 25;
        } else if (String.Equals(txt.text, "25x Speed")) {
            txt.text = "50x Speed";
            simSpeed = 50;
        } else if (String.Equals(txt.text, "50x Speed")) {
            txt.text = "1x Speed";
            simSpeed = 1;
        } else {
            Debug.Log("Error: Unexpected value = " + txt.text + ". Setting to default");
            txt.text = "1x Speed";
            simSpeed = 1;
        }
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
        simSpeedBtn.interactable = false;
        pathTypeBtn.interactable = false;
        startBtn.interactable = false;
        pauseBtn.interactable = true;
        roomba.init(roombaSpeed, simSpeed, (int) batteryLife, pathType);
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
