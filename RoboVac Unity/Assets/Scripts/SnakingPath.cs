using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakingPath : Path
{
    private float MinimumSpeed = 5;
    private Vector3 currentDirection;

    //private bool bigAngleChange = false;

    public SnakingPath(float velocity, float simSpeed, Rigidbody2D vacuum){
        this.velocity = velocity;
        this.simSpeed = simSpeed;
        this.vacuum = vacuum;
    }

    public override void Move(){
        StartCoroutine(SnakingMove());
    }

    private IEnumerator SnakingMove(){
        // if(bigAngleChange){

        // }

        Launch();
        //float newY = 
        //yield return new WaitForSeconds(0);
        return null;
    }

    private float CalculateAngleChange(float newX, float newY){
        float angleChange = 0F;

        return angleChange;
    }

    public void Launch(float x = 0F, float y = 1F)
    {
        //The direction to be launched towards
        currentDirection = new Vector3(x, y, 0);

        //Make sure we start at the minimum speed limit
        Vector3 normalizedDirection = currentDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        vacuum.velocity = normalizedDirection;
    }

}
