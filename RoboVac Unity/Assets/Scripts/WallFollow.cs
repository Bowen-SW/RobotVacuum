using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollow : Path
{
    private float rightAngle = 90F;
    private bool inCollision = false;
    private bool isMovingAroundObj = false;

    public override void Move(){
        //If collision and sensor not touching, turn counterclockwise 90 degrees, then proceed.
        //else if collision and sensor is touching, turn 90 degrees clockwise, then proceed
        if(!inCollision){
            
            if(collisionObj.gameObject.tag == "chest" || collisionObj.gameObject.tag == "leg"){                
                StartCoroutine(MoveAroundObj());
            } else {
                isMovingAroundObj = false;
                Stop();

                if(isTouching){ //Turn Counter-clockwise
                    StartCoroutine(TurnCounter());   
                } else{ //Turn counter clockwise
                    StartCoroutine(TurnClock());
                }
            }
            
            isTouching = true;
        } else if(collisionObj.gameObject.tag == "North" || collisionObj.gameObject.tag == "East"
            || collisionObj.gameObject.tag == "South" || collisionObj.gameObject.tag == "West"){

            StopAllCoroutines();
            StartCoroutine(TurnCounter());
            isTouching = true;
            
        } else if(isMovingAroundObj){
            StopAllCoroutines();

            StartCoroutine(MoveAroundObj());
        }  
    }

    private IEnumerator MoveAroundObj(){
        inCollision = true;
        isMovingAroundObj = true;
        Backoff(-currentDirection.x, -currentDirection.y);
        yield return new WaitForSeconds(1F / velocity);

        Stop();

        //Turn CounterClockwise
        yield return TurnCounter();
        inCollision = true;

        //Give the roomba 2 seconds to try and get around the object
        yield return new WaitForSeconds(3F / velocity);
        
        Stop();
        //Turn Clockwise
        yield return TurnClock();
        inCollision = true;

        yield return new WaitForSeconds(4F / velocity);

        Stop();
        yield return TurnClock();


        isMovingAroundObj = false;
        inCollision = false;
    }

    private IEnumerator TurnClock(){
        inCollision = true;

        vacuum.angularVelocity = -angularVelocity;

        float waitTime = rightAngle / Mathf.Abs(angularVelocity);
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;

        int xPos = FindNextXClock();
        int yPos = FindNextYClock();
        Launch(xPos, yPos);
        inCollision = false;
    }

    private IEnumerator TurnCounter(){
        inCollision = true;

        vacuum.angularVelocity = angularVelocity;

        float waitTime = rightAngle / Mathf.Abs(angularVelocity);
        yield return new WaitForSeconds(waitTime);
        vacuum.angularVelocity = 0;

        int xPos = FindNextXCounter();
        int yPos = FindNextYCounter();
        Launch(xPos, yPos);
        inCollision = false;
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

        return roundedY;
    }
}