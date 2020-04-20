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
            if(col.IsTouching(wallSensor)
                   && (col.gameObject.tag == "North" || col.gameObject.tag == "West" || col.gameObject.tag == "East" || col.gameObject.tag == "South")){
                roomba.GetPath().SetIsTouching(true);
                ++count;
                // Debug.Log("Inc Count = " + count);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(roomba.GetPathType() == PathType.WallFollow){
            if(!col.IsTouching(wallSensor)
                    && (col.gameObject.tag == "North" || col.gameObject.tag == "West" || col.gameObject.tag == "East" || col.gameObject.tag == "South")){
                
                --count;
                // Debug.Log("Dec Count = " + count);
                if(count == 0){
                    roomba.GetPath().SetIsTouching(false);
                    roomba.GetPath().Move();
                    // Debug.Log("Wall follow not touching move");
                }
            }
        }
    }

    public void SetCount(int newCount){
        count = newCount;
    }

}