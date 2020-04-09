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
        if(roomba.GetPathType() == PathType.WallFollow){
            if(col.IsTouching(wallSensor) && (col.gameObject.tag != "whiskers" && col.gameObject.tag != "vacuum")
                   && (col.gameObject.tag == "North" || col.gameObject.tag == "West" || col.gameObject.tag == "East" || col.gameObject.tag == "South")){
                roomba.GetPath().SetIsTouching(true);
                ++count;
                Debug.Log("Count = " + count);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(roomba.GetPathType() == PathType.WallFollow){
            if(!col.IsTouching(wallSensor) && (col.gameObject.tag != "whiskers" && col.gameObject.tag != "vacuum")
                    && (col.gameObject.tag == "North" || col.gameObject.tag == "West" || col.gameObject.tag == "East" || col.gameObject.tag == "South")){
                
                --count;
                Debug.Log("Count = " + count);
                if(count == 0){
                    roomba.GetPath().SetIsTouching(false);
                    roomba.GetPath().Move();
                    Debug.Log("Wall follow move");
                }
            }
        }
    }

}