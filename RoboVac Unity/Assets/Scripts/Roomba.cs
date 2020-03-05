using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : MonoBehaviour
{
    public float MinimumSpeed = 20;

    //The Roomba's current random direction
    private Vector3 currentDirection;

    private float velocity;

    

    private Rigidbody2D vacuum;

    public Path path;

    private float factor;

    void Awake() {
        //vacuum = gameObject.AddComponent<Rigidbody2D>() as RigidBody2D;
        vacuum = GetComponent<Rigidbody2D>();

        Time.timeScale = 1F;
        //TODO: choose the path algorithm based on user selections
        //TODO: set the simulation speed based on user selection
        //path = gameObject.AddComponent<RandomPath>();
        path = gameObject.AddComponent<SnakingPath>();

    }

    //Handle the collision
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
 
    public void Launch()
    {
        path.SetFields(velocity, vacuum);
        //TODO: Current directions need to be based off of the roomba's current direction
        if(path is RandomPath){
            currentDirection = new Vector3(0, 1F, 0);
        } else if(path is SnakingPath) {
            currentDirection = new Vector3(0, 1F, 0);
        } else {
            //The direction to be launched towards
            currentDirection = new Vector3(0, 1F, 0);
        }

        path.SetDirection(currentDirection);

        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        vacuum.velocity = normalizedDirection;
        Debug.Log("Roomba Velocity = " + normalizedDirection);
    }

    public void SetVelocity(float robotSpeed){
        velocity = 50;
        Debug.Log("Velocity = " + velocity);
    }
 }
