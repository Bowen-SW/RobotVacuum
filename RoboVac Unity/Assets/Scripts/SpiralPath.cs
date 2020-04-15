using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralPath : RandomPath
{
    private Timer timer;
    private Roomba roomba;
    private bool isInRoutine = false;

    public override void Move(){
        roomba = GetComponent<Roomba>();
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
}