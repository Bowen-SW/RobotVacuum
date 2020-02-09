using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    protected float velocity;

    protected float simSpeed;

    protected Rigidbody2D vacuum;

    protected Vector3 currentDirection;

    public abstract void Move();

    public abstract void Launch(float x = 0, float y = 0);

    public void SetFields (float velocity, float simSpeed, Rigidbody2D vacuum){ 
        this.velocity = velocity;
        this.simSpeed = simSpeed;
        this.vacuum = vacuum;
    }

    public void SetDirection(Vector3 direction){
        this.currentDirection = direction;
    }

    protected void Stop(){
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
    
}
