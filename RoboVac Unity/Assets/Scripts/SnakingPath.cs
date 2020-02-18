using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingPath : Path
{
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
            float temp = x;
            x = y;
            y = -temp;
            turn90 = true;
            velocity = -velocity;

            yield break;
        } else {
            inCurrentCollision = true;
        }

        //Start Logic for turning to be parallel with wall
        Stop();

        angleChange = TurnParallel();

        vacuum.angularVelocity = velocity;
        // vacuum.angularVelocity = velocity * simSpeed;

        //Reset velocity to be positive
        velocity = Mathf.Abs(velocity); 

        waitTime = angleChange / velocity;       
        
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;
        //End Logic for turning to be parallel with wall

        
        if(counterClockwise){
            Launch(-x, 0);
            
            yield return new WaitForSeconds(.2F/factor);

            //If a collision happens here, it will be detected and dealt with

            Stop();

            angleChange = 45F;
            if(turn90){
                angleChange += 180F;
            }
            
            vacuum.angularVelocity = velocity;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime);
            vacuum.angularVelocity = 0;

            Launch(-x, -y);
            
        } else { //Turn clockwise
            Launch(0, y);
            yield return new WaitForSeconds(.2F/factor);
            
            //If a collision happens here, it will be detected and dealt with

            Stop();
            angleChange = 45F;
            if(turn90){
                angleChange += 180F;
            }

            vacuum.angularVelocity = -velocity;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime);
            vacuum.angularVelocity = 0;

            Launch(x, y);
        }
            
        inCurrentCollision = false;
        turn90 = false;
    }

    private float TurnParallel() {
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
            velocity = velocity * -1;
        } else {
            //Should be an error
        } 



        return angleChange;
    }

    

}
