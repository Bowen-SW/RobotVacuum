using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : Path
{
    public override void Move(){
        StartCoroutine(RandomMove());
    }

    private IEnumerator RandomMove(){
        Backoff(-currentDirection.x, -currentDirection.y);
        yield return new WaitForSeconds(.1F);
        Stop();

        float randomX = Random.Range(-1F, 1F);
        float randomY = Random.Range(-1F, 1F);

        float angleChange = CalculateAngleChange(randomX, randomY);

        vacuum.angularVelocity = angularVelocity;

        float waitTime = angleChange / Mathf.Abs(angularVelocity);        
        
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;

        Launch(randomX, randomY);
    }

    private float CalculateAngleChange(float randomX, float randomY){
        float currentAngle = GetCurrentAngle();

        float newAngle = Mathf.Atan(randomY / randomX) * 180 / Mathf.PI;

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
        angularVelocity = Mathf.Abs(angularVelocity);

        if(angleChange > 180){
            //Rotate clockwise for a shorter length than it would take to go counterclockwise
            angularVelocity = -1 * angularVelocity;
            angleChange = 360 - angleChange;
        } 

        return angleChange;
    }
    public void Backoff(float x, float y)
    {
        //The direction to be launched towards
        Vector3 direction = new Vector3(x, y, 0);
        Vector3 normalizedDirection = direction.normalized * velocity;
        vacuum.velocity = normalizedDirection;
    }    
 
}
