using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    
    Roomba roomba = null;
    public GameObject room;
    private Room _room;

    // Start is called before the first frame update
    void Start()
    {
        _room = room.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(roomba == null)
        {
            roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        }
        else if(Object.FindObjectOfType<Simulation>().IsPlaying())
        {
            if(other.gameObject.tag == "whiskers")
            {
                _room.WhiskerCell(roomba);
            }
            else if(other.gameObject.tag == "vacuum")
            {
                _room.VacuumCell(roomba);
            }
        }
    }

}
