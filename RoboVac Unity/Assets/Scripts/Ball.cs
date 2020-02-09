using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float MinimumSpeed = 5;
    public float MaximumSpeed = 10;

    //Speed of the simulation
    private float simSpeed = 1f;

    //The Roomba's current random direction
    private Vector3 currentDirection;

    private float velocity = 60F;

    //Handle the collision
    void OnCollisionEnter2D(Collision2D col) {
        StartCoroutine(RandomPath());
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        gameObject.transform.Translate(moveHorizontal , moveVertical , 0.0f);
    }

    private float CalculateAngleChange(float randomX, float randomY){
        float currentAngle;

        //Check for angles in which arc-tangent is undefined
        if(currentDirection.x == 0 && currentDirection.y > 0){
            currentAngle = 90;
        } else if (currentDirection.x == 0 && currentDirection.y < 0){
            currentAngle = 270;
        } else {
            //Find the angles in degrees using arc-tangent
            currentAngle = Mathf.Atan(currentDirection.y / currentDirection.x) * 180 / Mathf.PI;
        }

        float newAngle = Mathf.Atan(randomY / randomX) * 180 / Mathf.PI;

        //The new angle will depend on whether the new direction coordinates are each positive or negative
        if(currentDirection.x < 0) { //2nd and 3rd quadrant
            currentAngle = currentAngle + 180;
        } else if (currentDirection.y < 0) { //4th quadrant
            currentAngle = currentAngle + 360;
        } else { //1st quandrant
            //no change needed
        }

        //The new angle will depend on whether the new direction coordinates are each positive or negative
        if(randomX < 0) { //2nd and 3rd quadrant
            newAngle = newAngle + 180;
        } else if (randomY < 0) { //4th quadrant
            newAngle = newAngle + 360;
        } else { //1st quandrant
            //no change needed
        }


        float angleChange = newAngle - currentAngle;

        //Make sure the change is positive
        if(angleChange < 0){
            angleChange = angleChange + 360;
        }

        //Reset the velocity to being positive
        if(velocity < 0) {
            velocity = -1 * velocity;
        }

        if(angleChange > 180){
            //Rotate clockwise for a shorter length than it would take to go counterclockwise
            velocity = -1 * velocity;
            angleChange = 360 - angleChange;
        } 

        Debug.Log("current (X,Y) = (" + currentDirection.x + ", " + currentDirection.y + ")");
        Debug.Log("random (X,Y) = (" + randomX + ", " + randomY + ")");
        Debug.Log("currentAngle = " + currentAngle + "\nnewAngle = " + newAngle + "\nangleChange = " + angleChange);  

        return angleChange;
    }

    private void Stop(){
        Vector3 direction = GetComponent<Rigidbody2D>().velocity;
        float speed = 0.0F;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void Rotate(float velocity, float angleChange){
        // GetComponent<Rigidbody2D>().angularVelocity = velocity * simSpeed;

        // float waitTime = angleChange / Mathf.Abs(velocity);        

        // yield return new WaitForSeconds(waitTime / simSpeed);
        // GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
 
    public void Launch(float x = 0, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);

        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        GetComponent<Rigidbody2D>().velocity = normalizedDirection;
    }

    IEnumerator RandomPath(){
        //float velocity = 60F;
        float randomX = Random.Range(-1F, 1F);
        float randomY = Random.Range(-1F, 1F);

        float angleChange = CalculateAngleChange(randomX, randomY);

        Stop();
        
        // Rotate(velocity, angleChange);

        GetComponent<Rigidbody2D>().angularVelocity = velocity * simSpeed;

        float waitTime = angleChange / Mathf.Abs(velocity);        

        yield return new WaitForSeconds(waitTime / simSpeed);
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        Launch(randomX, randomY);
    }
 }
