using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Chest
{
    private int ChairID;

    public int idCR
    {
        get
        {
            return ChairID;
        }
        set
        {
            ChairID = value;
            Debug.Log("Setting doorID for this room to: " + ChairID);
            UserInputInformation.AddStartVectorCR(ChairID, start);
            UserInputInformation.AddStopVectorCR(ChairID, stop);
        }
    }
}
