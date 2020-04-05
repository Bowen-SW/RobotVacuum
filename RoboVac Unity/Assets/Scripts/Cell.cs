using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Floor floor;


    // Start is called before the first frame update
    void Start()
    {
        floor = transform.GetChild(0).gameObject.GetComponent<Floor>();
    }

    public void ResetFloor()
    {
        floor.ResetFloor();
    }

    public float GetCoverage()
    {
        return floor.coverage;
    }

}
