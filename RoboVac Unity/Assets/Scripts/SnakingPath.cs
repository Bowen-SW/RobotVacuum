using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingPath : Path
{
    private float MinimumSpeed = 5;

    private int offset = 0;

    private bool turn90 = false; //Turn 90 degrees if true

    private bool firstTurn = true;
    private bool counterClockwise = false; //Turn counter clockwise if true
    private bool clockwise = false; //Turn clockwise if true

    private bool inCurrentCollision = false;

    float x = 1F;
    float y = 1F;

    public override void Move(){
        StartCoroutine(SnakingMove());
    }

    private IEnumerator SnakingMove(){
        float angleChange;
        float waitTime;
        
        if(inCurrentCollision) {
            //offset = 90;
            x = -1F;
            y = 1F;
            turn90 = true;
            velocity = -velocity;
            yield break;
        } else {
            inCurrentCollision = true;
        }

        //Start Logic for turning to be parallel with wall
        Stop();

        angleChange = TurnParallel();

        vacuum.angularVelocity = velocity * simSpeed;

        //Reset velocity to be positive
        velocity = Mathf.Abs(velocity); 

        waitTime = angleChange / velocity;       
        
        yield return new WaitForSeconds(waitTime / simSpeed);
        vacuum.angularVelocity = 0;
        //End Logic for turning to be parallel with wall

        
        if(counterClockwise){
            Launch(-x, 0);
            //Need to detect if a collision occurs here. If so, apply offset
            
            yield return new WaitForSeconds(.1F);
            Stop();

            angleChange = 45F;
            if(turn90){
                angleChange += 90F;
            }
            
            vacuum.angularVelocity = velocity * simSpeed;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime / simSpeed);
            vacuum.angularVelocity = 0;

            Launch(-x, -y);

            // if(offset == 0){
            //     Launch(x, y);
            // } else {
            //     Launch(y, -x);
            //     velocity = -velocity;
            // }
            
        } else { //Turn clockwise
            Launch(0, y);
            yield return new WaitForSeconds(.1F);
            //TODO: Detect if a collision happens here


            Stop();
            angleChange = 45F;
            if(turn90){
                angleChange += 90F;
            }

            vacuum.angularVelocity = -velocity * simSpeed;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime / simSpeed);
            vacuum.angularVelocity = 0;

            Launch(x, y);

            // if(offset == 0){
            //     Launch(x, y);
            // } else {
            //     Launch(y, -x);
            //     velocity = -velocity;
            // }
        }
            
        inCurrentCollision = false;
        turn90 = false;
        offset = 0;
    }

    private float TurnParallel() {
        float angleChange = 0F;
        float currentAngle = GetCurrentAngle();

        //See if Raycast can be used so that it always is able to rotate to parallel

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
            velocity = velocity * -1;
        } else {
            //Should be an error
        } 

        return angleChange;
    }

    private void BottomLeft(){
        Launch(-1F, -1F);
    }

    private void BottomRight(){
        Launch(1F, -1F);
    }

    private void TopLeft(){
        Launch(-1F, 1F);
    }

    private void TopRight(){
        Launch(1F, 1F);
    }

    public override void Launch(float x = 0F, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);

        //TODO: Fix the normalized speed. Speed should be the same at all times
        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        vacuum.velocity = normalizedDirection;
    }

}
