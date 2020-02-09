using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float MinimumSpeed = 5;

    //Speed of the simulation
    private float simSpeed = 1f;

    //The Roomba's current random direction
    private Vector3 currentDirection;

    private float velocity = 60F;

    private Rigidbody2D vacuum;

    private Path path;

    void Awake() {
        //vacuum = gameObject.AddComponent<Rigidbody2D>() as RigidBody2D;
        vacuum = GetComponent<Rigidbody2D>();

        //TODO: choose the path algorithm based on user selections
        //TODO: set the simulation speed based on user selection
        path = gameObject.AddComponent<RandomPath>();
        path.SetFields(velocity, simSpeed, vacuum);
        //path = new RandomPath(velocity, simSpeed);
        //path = new SnakingPath(velocity, simSpeed, vacuum);
    }

    //Handle the collision
    void OnCollisionEnter2D(Collision2D col) {
        //StartCoroutine(RandomPath());
        path.Move();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        gameObject.transform.Translate(moveHorizontal , moveVertical , 0.0f);
    }
 
    public void Launch(float x = 0, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);

        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        vacuum.velocity = normalizedDirection;
    }
 }
