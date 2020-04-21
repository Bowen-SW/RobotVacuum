using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollowDoorTrigger : MonoBehaviour
{
    // private CapsuleCollider2D _doorTrigger;
    private Roomba _roomba = null;
    private bool isWallFollow = false;
    private Door _door;
    private DoorSpace _doorSpace;
    private BoxCollider2D wallSensor;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponentInParent<Door>();

        if(_roomba == null){
            _roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        }
    }

    // void Update()
    // {
        
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        isWallFollow = (_roomba.GetPathType() == PathType.WallFollow);
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roomba" && isWallFollow)
            {
                // List<GameObject> walls = _door.AllWalls();
                // foreach(GameObject wall in walls)
                // {
                //     Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), _roomba.GetComponent<CircleCollider2D>());
                // }
                // Debug.Log("WallFollowDoorTrigger Enter and move");
                _roomba.GetPath().SetIsTouching(false);
                _roomba.GetPath().Move();
            }
        }
    }
}
