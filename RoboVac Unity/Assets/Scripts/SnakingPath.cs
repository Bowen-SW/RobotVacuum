using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingPath : Path
{
    private bool shift = false; //True if it hits an unexpected collision

    private bool firstTurn = true;
    private bool counterClockwise = false; //Turn counter clockwise if true
    private bool clockwise = false; //Turn clockwise if true
    private bool inCurrentCollision = false;
    //private bool inCoroutine = false;

    float xDirection = 1F;
    float yDirection = 1F;

    public override void Move(){
        StartCoroutine(SnakingMove());
    }

    private IEnumerator SnakingMove(){
        Backoff(-currentDirection.x, -currentDirection.y);
        Stop();
        
        if(inCurrentCollision) {
            float temp = xDirection;
            xDirection = yDirection;
            yDirection = -temp;
            shift = true;
            angularVelocity = -angularVelocity;

            yield break;
        } else {
            inCurrentCollision = true;
        }

        //Start Logic for turning to be parallel with wall
        float angleChange = FindParallel();

        vacuum.angularVelocity = angularVelocity;
        
        angularVelocity = Mathf.Abs(angularVelocity); //Reset velocity to be positive

        float waitTime = angleChange / angularVelocity;       
        
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;
        //End Logic for turning to be parallel with wall
        
        if(counterClockwise){
            //StartCoroutine(TurnCounterClock()); 
            Launch(-xDirection, 0);
            
            yield return new WaitForSeconds(1F / velocity);
            //If a collision happens here, it will be detected and dealt with using the inCurrentCollision boolean

            Stop();

            angleChange = 45F;
            if(shift){
                angleChange += 180F;
            }
            
            vacuum.angularVelocity = angularVelocity;

            waitTime = angleChange / Mathf.Abs(angularVelocity);        
            
            yield return new WaitForSeconds(waitTime);
            vacuum.angularVelocity = 0;

            Launch(-xDirection, -yDirection);           
        } else { //Turn clockwise
            //StartCoroutine(TurnClockwise());
            Launch(0, yDirection);
            yield return new WaitForSeconds(1F / velocity);
            //If a collision happens here, it will be detected and dealt with using the inCurrentCollision boolean

            Stop();
            angleChange = 45F;
            if(shift){
                angleChange += 180F;
            }

            vacuum.angularVelocity = -angularVelocity;

            waitTime = angleChange / Mathf.Abs(angularVelocity);        
            
            yield return new WaitForSeconds(waitTime);
            vacuum.angularVelocity = 0;

            Launch(xDirection, yDirection);
        }

        // while(inCoroutine){
        //     yield return new WaitForSeconds(0.1F);
        // }
            
        inCurrentCollision = false;
        shift = false;
    }
    

    private float FindParallel() {
        float angleChange = 0F;
        float currentAngle = GetCurrentAngle();

        if(firstTurn){
            angleChange = 90F;
            firstTurn = false;
            counterClockwise = true;
        } else if (clockwise){
            angleChange = 135F;
            counterClockwise = true;
            clockwise = false;
        } else if (counterClockwise) {
            angleChange = 135;
            counterClockwise = false;
            clockwise = true;
            angularVelocity = angularVelocity * -1;
        } else {
            Debug.Log("Error trying to turn parallel in Snaking Path");
        } 

        return angleChange;
    }

    public void Backoff(float x, float y)
    {
        Debug.Log("Backoff");
        //The direction to be launched towards
        Vector3 direction = new Vector3(x, y, 0);
        Vector3 normalizedDirection = direction.normalized * velocity;
        vacuum.velocity = normalizedDirection;
    }   

    // private IEnumerator TurnCounterClock(){
    //     Launch(-xDirection, 0);
            
    //     yield return new WaitForSeconds(.2F);
    //     //If a collision happens here, it will be detected and dealt with using the inCurrentCollision boolean

    //     Stop();

    //     float angleChange = 45F;
    //     if(shift){
    //         angleChange += 180F;
    //     }
        
    //     vacuum.angularVelocity = angularVelocity;

    //     float waitTime = angleChange / Mathf.Abs(angularVelocity);        
        
    //     yield return new WaitForSeconds(waitTime);
    //     vacuum.angularVelocity = 0;

    //     Launch(-xDirection, -yDirection);
    // }

    // private IEnumerator TurnClockwise(){
    //     Launch(0, yDirection);
    //     yield return new WaitForSeconds(.2F);
    //     //If a collision happens here, it will be detected and dealt with using the inCurrentCollision boolean

    //     Stop();
    //     float angleChange = 45F;
    //     if(shift){
    //         angleChange += 180F;
    //     }

    //     vacuum.angularVelocity = -angularVelocity;

    //     float waitTime = angleChange / Mathf.Abs(angularVelocity);        
        
    //     yield return new WaitForSeconds(waitTime);
    //     vacuum.angularVelocity = 0;

    //     Launch(xDirection, yDirection);
    // }
}
