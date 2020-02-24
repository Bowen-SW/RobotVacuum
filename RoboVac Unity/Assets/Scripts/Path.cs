using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    protected float velocity;

    protected float simSpeed;

    protected float factor;

    protected Rigidbody2D vacuum;

    protected Vector3 currentDirection;

    protected float waitFactor;

    public abstract void Move();

    //public abstract void Launch(float x = 0, float y = 0);

    public void SetFields (float velocity, float simSpeed, float factor, Rigidbody2D vacuum){ 
        this.velocity = velocity;
        this.simSpeed = simSpeed;
        this.vacuum = vacuum;
        this.factor = factor;
        SetWaitFactor((int)velocity);
    }

    public void SetDirection(Vector3 direction){
        this.currentDirection = direction;
    }

    public void Stop(){
        Vector3 direction = vacuum.velocity;
        float speed = 0.0F;
        vacuum.velocity = direction * speed;
    }

    protected float GetCurrentAngle(){
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

        //The new angle will depend on whether the new direction coordinates are each positive or negative
        if(currentDirection.x < 0) { //2nd and 3rd quadrant
            currentAngle = currentAngle + 180;
        } else if (currentDirection.y < 0) { //4th quadrant
            currentAngle = currentAngle + 360;
        } else { //1st quandrant
            //no change needed
        }

        return currentAngle;
    }

    public void Launch(float x = 1F, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);

        float MinimumSpeed = 2f;
        //TODO: Fix the normalized speed. Speed should be the same at all times
        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        vacuum.velocity = normalizedDirection * factor;
        // Debug.Log("Roomba Velocity = " + normalizedDirection);
    }

    public void SetWaitFactor(int velocity){
        if(velocity == 150){            //6 x 25
            waitFactor = .5F;
        } else if (velocity == 300) {   //12 x 25
            waitFactor = 1F;
        } else if (velocity == 450) {   //18 x 25
            waitFactor = 1.25F;
        } else if (velocity == 600) {   //12 x 50
            waitFactor = 2F;
        } else if (velocity == 900) {   //18 x 50
            waitFactor = 2.5F;
        } else {                        //6-18 x 1
            waitFactor = .04F;
        }
    }
    
}
