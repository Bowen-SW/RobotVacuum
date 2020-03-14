using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralPath : RandomPath
{
    // float changeRate = 1F;
    // float waitTime = 3F;
    // float time = 0;
    // private bool inCurrentCollision = false;
    // public override void Move(){
    //     time = 0;
    //    // StartCoroutine(SpiralMove());
        
    // }

    // private IEnumerator SpiralMove(){
    //     //Launch in random direction, after X seconds of no collision then begin spiral.
    //     //Collision occurs, launch in random direction
    //     //Repeat

    //     if(inCurrentCollision) {
    //         yield break;
    //     } else {
    //         inCurrentCollision = true;
    //     }

    //     Backoff(-currentDirection.x, -currentDirection.y);
    //     yield return new WaitForSeconds(.1F);
    //     Stop();

    //     float randomX = Random.Range(-1F, 1F);
    //     float randomY = Random.Range(-1F, 1F);

    //     float angleChange = CalculateAngleChange(randomX, randomY);

    //     vacuum.angularVelocity = angularVelocity;

    //     float waitTime = angleChange / Mathf.Abs(angularVelocity);        
        
    //     yield return new WaitForSeconds(waitTime);
    //     vacuum.angularVelocity = 0;

    //     Launch(randomX, randomY);

    //     yield return new WaitForSeconds(3F);

    //     Debug.Log("Start Spiral");
        


    //     inCurrentCollision = false;
    // }

    // protected float CalculateAngleChange(float randomX, float randomY){
    //     float currentAngle = GetCurrentAngle();

    //     float newAngle = Mathf.Atan(randomY / randomX) * 180 / Mathf.PI;

    //     //The new angle will depend on whether the new direction coordinates are each positive or negative
    //     if(randomX < 0) { //2nd and 3rd quadrant
    //         newAngle = newAngle + 180;
    //     } else if (randomY < 0) { //4th quadrant
    //         newAngle = newAngle + 360;
    //     } else { //1st quandrant
    //         //no change needed
    //     }

    //     float angleChange = newAngle - currentAngle;

    //     //Make sure the change is positive
    //     if(angleChange < 0){
    //         angleChange = angleChange + 360;
    //     }

    //     //Reset the velocity to being positive
    //     angularVelocity = Mathf.Abs(angularVelocity);

    //     if(angleChange > 180){
    //         //Rotate clockwise for a shorter length than it would take to go counterclockwise
    //         angularVelocity = -1 * angularVelocity;
    //         angleChange = 360 - angleChange;
    //     } 

    //     return angleChange;
    // }















    
}