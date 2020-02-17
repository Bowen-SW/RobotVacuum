using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RobotSpeedHandler : MonoBehaviour
{
    private float robotSpeed = 12F; 
    public void SetText(){
        Text txt = transform.Find("Text").GetComponent<Text>();

        if(String.Equals(txt, "12 in/sec")){
            Debug.Log("Click to 18 in/sec");
            txt.text = "18 in/sec";
            robotSpeed = 18F;
        } else if (String.Equals(txt, "18 in/sec")) {
            Debug.Log("Click to 6 in/sec");
            txt.text = "6 in/sec";
            robotSpeed = 6F;
        } else if (String.Equals(txt, "6 in/sec")) {
            Debug.Log("Click to 18 in/sec");
            txt.text = "12 in/sec";
            robotSpeed = 12F;
        } else {
            Debug.Log("Error: Unexpected value. Setting to default");
            txt.text = "12 in/sec";
            robotSpeed = 12F;
        }
    }

    public float GetRobotSpeed(){
        return robotSpeed;
    }
}
