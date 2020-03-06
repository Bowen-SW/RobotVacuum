using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roomba : MonoBehaviour
{
    public Text simTimeText;
    private Text timeText;
    private Rigidbody2D vacuum;

    private Path path;

    private bool timerStarted = false;
    float timer = 0F;

    void Awake() {
        vacuum = GetComponent<Rigidbody2D>();
        timeText = simTimeText.GetComponent<Text>();
        Time.timeScale = 1F;
    }

    public void init(float roombaSpeed, PathType pathType)
    {
        //TODO: set the simulation speed based on user selection
        //TODO: set the current direction based on where the roomba is pointing
        //TODO: set the "minimum speed" (launch velocity) in the path algorithm
        float angularVelocity = roombaSpeed * 4;

        SetPathType(pathType);
        path.SetFields(angularVelocity, vacuum);
        path.Launch();
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

            //TODO: Change to the user selected battery life
            if(Mathf.Floor(timer / 60) >= 1){
                Finish();
            }

            timeText.text = string.Format("{0}:{1}", minutes, seconds);          
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveHorizontal, moveVertical, 0.0F);
        transform.position += moveVector * Time.deltaTime;

        //gameObject.transform.Translate(moveHorizontal , moveVertical , 0.0f);
    }

    public void SetPathType(PathType pathType){
        Debug.Log("Path selection = " + pathType);

        switch(pathType){
            case PathType.Random:
                path = gameObject.AddComponent<RandomPath>();
                break;
            case PathType.Snaking:
                path = gameObject.AddComponent<SnakingPath>();
                break;
            case PathType.Spiral:
                //TODO
                break;
            case PathType.WallFollow:
                //TODO
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

    public void Resume(){ //TODO Add some sort of implementation
        Time.timeScale = 1F;
    }

    public void Finish(){
        Time.timeScale = 0F;
        //Debug.Log("Simulation Finished");
    }
 }
