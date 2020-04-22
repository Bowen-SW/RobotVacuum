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


    private void OnTriggerEnter2D(Collider2D other) {
        isWallFollow = (_roomba.GetPathType() == PathType.WallFollow);
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roomba" && isWallFollow && !_door.isClosed)
            {
                _roomba.GetPath().SetIsTouching(false);
                _roomba.GetPath().Move();
            }
        }
    }
}
