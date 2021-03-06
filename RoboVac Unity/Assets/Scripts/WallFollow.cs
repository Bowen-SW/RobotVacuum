using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollow : Path
{
    private float rightAngle = 90F;
    private bool firstTurn = true;
    public override void Move(){
        //If collision and sensor not touching, turn counterclockwise 90 degrees, then proceed.
        //else if collision and sensor is touching, turn 90 degrees clockwise, then proceed
        Backoff(-currentDirection.x, -currentDirection.y);
        Stop();

        if(isTouching || firstTurn){ //Turn clockwise
            
            StartCoroutine(TurnCounter());
            firstTurn = false;
            
        } else { //Turn counter clockwise
            StartCoroutine(TurnClock());
            
        }
    }

    private IEnumerator TurnClock(){
        // Debug.Log("Turning Clock");
        vacuum.angularVelocity = -angularVelocity;

        float waitTime = rightAngle / Mathf.Abs(angularVelocity);
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;

        int xPos = FindNextXClock();
        int yPos = FindNextYClock();
        Launch(xPos, yPos);
    }

    private IEnumerator TurnCounter(){
        // Debug.Log("Turning CounterClockwise");
        vacuum.angularVelocity = angularVelocity;

        float waitTime = rightAngle / Mathf.Abs(angularVelocity);
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;

        int xPos = FindNextXCounter();
        int yPos = FindNextYCounter();
        Launch(xPos, yPos);
    }

    private int FindNextXClock(){
        
        int roundedX = (int)Math.Round(currentDirection.x);
        int roundedY = (int)Math.Round(currentDirection.y);
        if(roundedX == -1 || roundedX == 1){
            roundedX = 0;
        } else if(roundedX == 0 && roundedY == -1){
            roundedX = -1;
        } else if(roundedX == 0 && roundedY == 1){
            roundedX = 1;
        } else {
            Debug.Log("Error finding next X clockwise position");
        }

        return roundedX;
    }
    private int FindNextYClock(){
        int roundedX = (int)Math.Round(currentDirection.x);
        int roundedY = (int)Math.Round(currentDirection.y);

        if(roundedY == -1 || roundedY == 1){
            roundedY = 0;
        } else if(roundedY == 0 && roundedX == -1){
            roundedY = 1;
        } else if(roundedY == 0 && roundedX == 1){
            roundedY = -1;
        } else {
            Debug.Log("Error finding next Y clockwise position");
        }

        return roundedY;
    }

    private int FindNextXCounter(){
        int roundedX = (int)Math.Round(currentDirection.x);
        int roundedY = (int)Math.Round(currentDirection.y);
        if(roundedX == -1 || roundedX == 1){
            roundedX = 0;
        } else if(roundedX == 0 && roundedY == -1){
            roundedX = 1;
        } else if(roundedX == 0 && roundedY == 1){
            roundedX = -1;
        } else {
            Debug.Log("Error finding next X clockwise position");
        }

        // Debug.Log("Current X = " + currentDirection.x + " New X = " + roundedX);
        return roundedX;
    }
    private int FindNextYCounter(){
        int roundedX = (int)Math.Round(currentDirection.x);
        int roundedY = (int)Math.Round(currentDirection.y);

        if(roundedY == -1 || roundedY == 1){
            roundedY = 0;
        } else if(roundedY == 0 && roundedX == -1){
            roundedY = -1;
        } else if(roundedY == 0 && roundedX == 1){
            roundedY = 1;
        } else {
            Debug.Log("Error finding next Y clockwise position");
        }

        // Debug.Log("Current Y = " + currentDirection.y + " New Y = " + roundedY);
        return roundedY;
    }
}