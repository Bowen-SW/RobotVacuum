using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    
    Roomba roomba = null;
    public float dirtiness = 1.0f;
    public float coverage
    {
        get
        {
            return 1.0f - dirtiness;
        }

        set {}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, dirtiness);
        if(dirtiness < 1.0f && Object.FindObjectOfType<Simulation>().IsStopped())
        {
            ResetFloor();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(roomba == null)
        {
            roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        }
        else if(Object.FindObjectOfType<Simulation>().IsPlaying())
        {
            if(other.gameObject.tag == "whiskers")
            {
                dirtiness *= 1.0f - (roomba.GetWhiskerEff()/100.0f);
            }
            else if(other.gameObject.tag == "vacuum")
            {
                dirtiness *= 1.0f - (roomba.GetVacEff()/100.0f);
            }
        }
    }
    
    public void ResetFloor() {
        dirtiness = 1.0f;
    }

}
