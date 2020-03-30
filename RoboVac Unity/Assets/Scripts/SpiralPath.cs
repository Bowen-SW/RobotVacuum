using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralPath : RandomPath
{
    //roomba.SetSpiral(true); //after travelling a distance for x seconds
    private Timer timer;
    private Roomba roomba;
    private bool isInRoutine = false;

    public override void Move(){
        roomba = GetComponent<Roomba>();
        //SetTimer();
        //Debug.Log("Timer set");
        StartCoroutine(RandomMove());
        StartCoroutine(WaitToSpiral());
    }

    private IEnumerator WaitToSpiral(){
        if(isInRoutine){
            yield break;
        }

        isInRoutine = true;
        yield return new WaitForSeconds(7F/velocity);
        Stop();
        roomba.SetDoSpiral(true);
        Debug.Log("Sprial set to true");

        isInRoutine = false;
    }

    // private void SetTimer() {
    //     timer = new Timer(5000);
    //     timer.Elapsed += OnTimedEvent;
    //     timer.Enabled = true;
    // }    

    // private void OnTimedEvent(object source, ElapsedEventArgs e){
    //     Stop();
    //     timer.Stop();
    //     timer.Dispose();
    //     Debug.Log("Set do sprial");
    //     roomba.SetDoSpiral(true);
    // }
}