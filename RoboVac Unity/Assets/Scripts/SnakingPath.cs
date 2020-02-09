using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingPath : Path
{
    private float MinimumSpeed = 5;

    private bool bigAngleChange = false;

    private bool firstTurn = true;
    private bool last120 = false; //True if it had to turn 120 degrees on its last turn
    private bool last150 = false; //True if it had to turn 150 degrees on its last turn

    public override void Move(){
        StartCoroutine(SnakingMove());
    }

    private IEnumerator SnakingMove(){
        float angleChange;
        // if(bigAngleChange){
        //     angleChange = 60F;
        // } else {
        //     angleChange = 30F;
        // }

        //Logic for turning to be parallel with wall
        
        Stop();

        angleChange = TurnParallel();

        vacuum.angularVelocity = velocity * simSpeed;

        //Reset velocity to be positive
        velocity = Mathf.Abs(velocity);

        float waitTime = angleChange / velocity;        
        
        yield return new WaitForSeconds(waitTime / simSpeed);
        vacuum.angularVelocity = 0;

        if(last150 || !last120){
            Launch(-1F, 0);
            yield return new WaitForSeconds(.1F);
            Stop();

            angleChange = 30F;
            vacuum.angularVelocity = velocity * simSpeed;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime / simSpeed);
            vacuum.angularVelocity = 0;

            Launch(-1F, -.577F);
        } else { //Last turn was 120 degrees
            Launch(0, 1F);
            yield return new WaitForSeconds(.1F);
            Stop();
            angleChange = 60F;
            vacuum.angularVelocity = -velocity * simSpeed;

            waitTime = angleChange / Mathf.Abs(velocity);        
            
            yield return new WaitForSeconds(waitTime / simSpeed);
            vacuum.angularVelocity = 0;

            Launch(.866F, .5F);
        }
        
        
        
    }

    private float TurnParallel() {
        float angleChange = 0F;
        float currentAngle = GetCurrentAngle();

        if(firstTurn){
            angleChange = 90F;
            firstTurn = false;
            last150 = true;
        } else if (last120){
            angleChange = 150F;
            last150 = true;
            last120 = false;
        } else if (last150) {
            angleChange = 120F;
            last150 = false;
            last120 = true;
            velocity = velocity * -1;
        } else {
            //Should be an error
        } 





        return angleChange;
    }

    // private float CalculateAngleChange(float newX, float newY){
    //     float angleChange;

    //     if(bigAngleChange){ //

    //     } else {

    //     }


    //     return angleChange;
    // }

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
