using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollowDoorTrigger : MonoBehaviour
{
    private CapsuleCollider2D _doorTrigger;
    private Roomba _roomba = null;
    private bool isWallFollow = false;
    private Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponentInParent<Door>();
        _doorTrigger = GetComponent<CapsuleCollider2D>();
        if(_roomba == null){
            _roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        }
    }

    void Update()
    {
        isWallFollow = (_roomba.GetPathType() == PathType.WallFollow);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roombaRear" && isWallFollow)
            {
                List<GameObject> walls = _door.AllWalls();
                foreach(GameObject wall in walls)
                {
                    wall.GetComponent<EdgeCollider2D>().enabled = false;
                    //This causes the roomba to not be touching anything and it will turn clockwise
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roombaRear" && isWallFollow)
            {
                List<GameObject> walls = _door.AllWalls();
                foreach(GameObject wall in walls)
                {
                    Debug.Log("Enable walls");
                    wall.GetComponent<EdgeCollider2D>().enabled = true;
                }
                _roomba.GetPath().SetIsTouching(false);
                _roomba.GetPath().Move();
            }
        }
    }
}
