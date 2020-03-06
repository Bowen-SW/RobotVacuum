using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : MonoBehaviour
{
    private Rigidbody2D vacuum;

    private Path path;

    void Awake() {
        vacuum = GetComponent<Rigidbody2D>();
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
    }

    void OnCollisionEnter2D(Collision2D col) { 
        path.Move();
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
 }
