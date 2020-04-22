using System.Collections;
using UnityEngine;



public class WallFollowTrigger : MonoBehaviour {
    
    public Collider2D wallSensor;
    private int count = 0;
    private Roomba roomba;
    private Path path;

    void Start() {
        roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        path = roomba.GetPath();   
    }
    
    public void SetCount(int newCount){
        count = newCount;
    }

}