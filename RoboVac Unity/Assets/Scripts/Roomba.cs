using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roomba : MonoBehaviour, IMovable
{
    public TMP_Text simTimeText;
    private TMP_Text timeText;
    private Rigidbody2D vacuum;
    private int batteryLife;
    private Path path;
    private PathType pathType;
    private float vacEff;
    private float whiskerEff;
    private float roombaSpeed;
    private bool timerStarted = false;
    private bool doSprial = true;
    private bool timeLimitReached = false;
    private float simSpeed = 1F;
    private float timer = 0F;
    private float unit = .1F;
    private float angle = 55F;
    private float xCoordinate = 0;
    private float yCoordinate = 0;

    private Vector3 target;
    private bool moving = false;

    //IMovable functions
    public void SetTarget(Vector2 position)
    {
        target = new Vector3(position.x, position.y, transform.position.z);
    }

    public Vector2 GetTarget()
    {
        return new Vector3(target.x, target.y);
    }

    public void SetMoving(bool move)
    {
        moving = move;
    }

    public bool IsMoving()
    {
        return moving;
    }

    private void SetDefaults(){
        timerStarted = false;
        doSprial = true;
        timeLimitReached = false;
    }

    void Awake() {
        vacuum = GetComponent<Rigidbody2D>();
        timeText = simTimeText.GetComponent<TMP_Text>();
    }

    public void Init(float roombaSpeed, int batteryLife, PathType pathType, float vacEff, float whiskerEff)
    {
        timeLimitReached = false;
        SetDefaults();
        //TODO: set the current direction based on where the roomba is pointing
        Time.timeScale = simSpeed;      //Sets the simulation speed
        this.batteryLife = batteryLife; 
        this.whiskerEff = whiskerEff;
        this.vacEff = vacEff;
        this.roombaSpeed = roombaSpeed;

        SetStartPosition();

        SetPathType(pathType);
        path.SetFields(this.roombaSpeed, vacuum);

        if(pathType == PathType.Spiral){
            path.SetDirection(new Vector3(0, 1F, 0));
        } else {
            path.Launch();
        }

        timerStarted = true;
    }

    void OnCollisionEnter2D(Collision2D col) { 
        if(pathType == PathType.Spiral){
            doSprial = false;
        }
        path.Move();
    }

    void Update(){
        if(timerStarted){
            if(Selection.selected == this.gameObject)
            {
                Selection.selected = null;
            }

            timer = timer + Time.deltaTime;

            string hours = Math.Floor(timer / 3600).ToString("00");
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");

            if(Mathf.Floor(timer / 60) >= 3){
                timeLimitReached = true;
                // Stop();
            }

            timeText.text = string.Format("{0}:{1}:{2}", hours, minutes, seconds); 
            UserInputInformation.durationGS = string.Format("{0}:{1}:{2}", hours, minutes, seconds);          
        }
        else if(moving)
        {
            transform.position = target;
        }
    }

    void FixedUpdate()
    {
        if(pathType == PathType.Spiral && doSprial){
            Vector3 moveVector = new Vector3(unit*(float)Math.Cos(timer), unit*(float)Math.Sin(timer),0);
            transform.position += moveVector * Time.deltaTime;
            unit += Time.deltaTime / 15F;
            transform.Rotate(Vector3.forward, angle * Time.deltaTime);
            //vacuum.angularVelocity = vacuum.angularVelocity - (Time.deltaTime * .1F);    
        }
        else{        
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 moveVector = new Vector3(moveHorizontal, moveVertical, 0.0F);
            transform.position += moveVector * Time.deltaTime;
        }
    }

    public void SetPathType(PathType pathType){
        Debug.Log("Path selection = " + pathType);
        //PathType result;
        //Enum.TryParse(pathType, out result);
        switch(pathType){
            case PathType.Random:
                path = gameObject.AddComponent<RandomPath>();
                this.pathType = PathType.Random;
                UserInputInformation.pathTypeGS = "Random";
                break;
            case PathType.Snaking:
                path = gameObject.AddComponent<SnakingPath>();
                this.pathType = PathType.Snaking;
                UserInputInformation.pathTypeGS = "Snaking";
                break;
            case PathType.Spiral:
                path = gameObject.AddComponent<SpiralPath>();
                this.pathType = PathType.Spiral;
                UserInputInformation.pathTypeGS = "Spiral";
                break;
            case PathType.WallFollow:
                path = gameObject.AddComponent<WallFollow>();
                this.pathType = PathType.WallFollow;
                UserInputInformation.pathTypeGS = "Wall Follow";
                break;
            case PathType.All:
                this.pathType = PathType.All;
                UserInputInformation.pathTypeGS = "All";
                break;
            default:
                Debug.Log("Error setting path. Default to Random.");
                path = gameObject.AddComponent<RandomPath>();
                this.pathType = PathType.Random;
                UserInputInformation.pathTypeGS = "Random";
                break;
        }
    }

    public void Pause(){
        Time.timeScale = 0F;
    }

    public void Resume(){
        Time.timeScale = simSpeed;
    }

    public void Stop(){
        Time.timeScale = 0F;
        Debug.Log("Simulation Stopped");
        SaveRunInfo();
        vacuum.position = new Vector2(xCoordinate, yCoordinate);
        vacuum.rotation = 0F;
        transform.position = new Vector3(xCoordinate, yCoordinate, 0);
        transform.rotation = Quaternion.identity;
        unit = .1F;
    }

    public Path GetPath(){
        return path;
    }

    public PathType GetPathType(){
        return pathType;
    }

    public void SetDoSpiral(bool spiral){
        doSprial = spiral;
        //Reset the spiral values
        unit = .1F;
    }

    public float GetVacEff(){
        return vacEff;
    }

    public void SetVacEff(float eff){
        vacEff = eff;
    }

    public float GetWhiskerEff(){
        return whiskerEff;
    }

    public void SetWhiskerEff(float eff){
        whiskerEff = eff;
    }

    private void SetStartPosition(){
        xCoordinate = vacuum.position.x;
        yCoordinate = vacuum.position.y;
    }

    public void SetSimSpeed(float speed){
        simSpeed = speed;
        Time.timeScale = simSpeed;
    }

    public void SetBatteryLife(int batteryLife){
        this.batteryLife = batteryLife;
    }

    public void SetRoombaSpeed(float roombaSpeed){
        this.roombaSpeed = roombaSpeed;
    }

    public void ResetRunTime(){
        timer = 0;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        timeText.text = string.Format("{0}:{1}", minutes, seconds); 
    }

    public bool IsTimeLimitReached(){
        return timeLimitReached;
    }

    public void SetTimeLimitReached(bool limitReached){
        timeLimitReached = limitReached;
    }

    public void SaveRunInfo(){
        String timeStamp = GetTimestamp(DateTime.Now);

        Debug.Log("Duration: " + timeText.text);
        Debug.Log("Path Type: " + pathType);
        Debug.Log("Time Stamp: " + timeStamp);
    }

    private String GetTimestamp(DateTime value)
    {
        return value.ToString("MM:dd:yyyy");
    }
 }
