using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Roomba : MonoBehaviour
{
    public Text simTimeText;
    //public Collider2D wallSensor;
    private Text timeText;
    private Rigidbody2D vacuum;
    private int batteryLife;

    private Path path;
    private PathType pathType;

    private bool timerStarted = false;
    float timer = 0F;
    private int count = 0;

    float unit = .1F;
    float angle = 55F;
    bool isTouching = false;

    void Awake() {
        vacuum = GetComponent<Rigidbody2D>();
        timeText = simTimeText.GetComponent<Text>();
    }

    public void init(float roombaSpeed, float simSpeed, int batteryLife, PathType pathType)
    {
        //TODO: set the current direction based on where the roomba is pointing
        Time.timeScale = simSpeed;      //Sets the simulation speed
        this.batteryLife = batteryLife;

        SetPathType(pathType);
        path.SetFields(roombaSpeed, vacuum);

        if(pathType != PathType.Spiral){
            path.Launch();
        } else {
            //vacuum.angularVelocity = 45F * simSpeed;
        }

        timerStarted = true;

        
    }

    void OnCollisionEnter2D(Collision2D col) { 
        path.Move();
    }

    void Update(){
        if(timerStarted){

            timer = timer + Time.deltaTime;

            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");

            //TODO: Change to the user selected battery life for production
            //if(Mathf.Floor(timer / 60) >= batterLife){
            if(Mathf.Floor(timer / 60) >= 1){
                Finish();
            }

            timeText.text = string.Format("{0}:{1}", minutes, seconds);          
        }
    }

    void FixedUpdate()
    {
        if(pathType == PathType.Spiral){
            //Debug.Log("Spiraling");
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
            default:
                Debug.Log("Error setting path. Default to Random.");
                path = gameObject.AddComponent<RandomPath>();
                break;
        }
    }

    public void Pause(){
        Time.timeScale = 0F;
    }

    public void Resume(){
        Time.timeScale = 1F;
    }

    public void Finish(){
        Time.timeScale = 0F;
        //Debug.Log("Simulation Finished");
    }

    public Path GetPath(){
        return path;
    }

 }
