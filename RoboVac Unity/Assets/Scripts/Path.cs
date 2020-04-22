using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    protected float angularVelocity;

    public float velocity;

    protected Collision2D collisionObj;

    protected Rigidbody2D vacuum;

    protected Vector3 currentDirection;
    protected bool isTouching = true;

    public abstract void Move();

    public void SetFields (float roombaSpeed, Rigidbody2D vacuum){ 
        float velocityFactor = roombaSpeed / 12F;
        this.angularVelocity = roombaSpeed * 4 * velocityFactor;
        velocity = velocityFactor;
        this.vacuum = vacuum;
    }

    public void SetDirection(Vector3 direction){
        this.currentDirection = direction;
    }

    public void Stop(){
        Vector3 direction = vacuum.velocity;
        float speed = 0.0F;
        vacuum.velocity = direction * speed;
    }

    public void StopThreads(){
        StopAllCoroutines();
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

    public void Launch(float x = 0F, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);
        Vector3 normalizedDirection = currentDirection.normalized * velocity;
        vacuum.velocity = normalizedDirection;
    }    

    public void SetIsTouching(bool touch){
        isTouching = touch;
    }

    protected void Backoff(float x, float y)
    {
        //The direction to be launched towards
        Vector3 direction = new Vector3(x, y, 0);
        Vector3 normalizedDirection = direction.normalized * velocity;
        vacuum.velocity = normalizedDirection;
    }    

    public void SetCollisionObj(Collision2D obj){
        collisionObj = obj;
    }
}
