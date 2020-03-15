using System.Collections;
using UnityEngine;



public class WallFollowTrigger : MonoBehaviour {
    
    public Collider2D wallSensor;
    private int count = 0;
    private Roomba roomba;
    private Path path;

    void Start() {
        roomba = GetComponentInParent<Roomba>();
        path = roomba.GetPath();   
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.IsTouching(wallSensor) && col.gameObject.tag != "whiskers" && col.gameObject.tag != "vacuum"){
            roomba.GetPath().SetIsTouching(true);
            ++count;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(!col.IsTouching(wallSensor) && col.gameObject.tag != "whiskers" && col.gameObject.tag != "vacuum"){
            roomba.GetPath().SetIsTouching(false);
            --count;
            if(count == 0){
                roomba.GetPath().Move();
            }
        }
    }

}