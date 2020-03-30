using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roomba : MonoBehaviour, IMovable
{
    public TMP_Text simTimeText;
    //public Collider2D wallSensor;
    private TMP_Text timeText;
    private Rigidbody2D vacuum;
    private int batteryLife;
    private Path path;
    private PathType pathType;
    private float vacEff = 75F;
    private float whiskerEff = 30F;
    private bool timerStarted = false;
    private bool doSprial = true;
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

    void Awake() {
        vacuum = GetComponent<Rigidbody2D>();
        timeText = simTimeText.GetComponent<TMP_Text>();
    }

    public void init(float roombaSpeed = 12f, float simSpeed = 1F, int batteryLife = 150, PathType pathType = PathType.Random)
    {
        //TODO: set the current direction based on where the roomba is pointing
        this.simSpeed = simSpeed;
        Time.timeScale = simSpeed;      //Sets the simulation speed
        this.batteryLife = batteryLife;

        SetStartPosition();

        SetPathType(pathType);
        path.SetFields(roombaSpeed, vacuum);

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

            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");

            if(Mathf.Floor(timer / 60) >= batteryLife){
                Stop();
            }

            timeText.text = string.Format("{0}:{1}", minutes, seconds);          
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
        this.pathType = pathType;
        switch(pathType){
            case PathType.Random:
                path = gameObject.AddComponent<RandomPath>();
                break;
            case PathType.Snaking:
                path = gameObject.AddComponent<SnakingPath>();
                break;
            case PathType.Spiral:
                path = gameObject.AddComponent<SpiralPath>();
                break;
            case PathType.WallFollow:
                path = gameObject.AddComponent<WallFollow>();
                break;
            case PathType.All:
                //TODO
                break;
            default: //TODO Default to all
                Debug.Log("Error setting path. Default to Random.");
                path = gameObject.AddComponent<RandomPath>();
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
        vacuum.position = new Vector2(xCoordinate, yCoordinate);
        vacuum.rotation = 0F;
        transform.position = new Vector3(xCoordinate, yCoordinate, 0);
        transform.rotation = Quaternion.identity;
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

    public void ResetRunTime(){
        timer = 0;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        timeText.text = string.Format("{0}:{1}", minutes, seconds); 
    }
 }
