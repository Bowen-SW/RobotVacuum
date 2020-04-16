using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpace : MonoBehaviour
{
    public GameObject door;
    public CapsuleCollider2D doorTrigger;
    private Door _door;
    private CapsuleCollider2D _doorTrigger;
    private Roomba _roomba = null;
    private bool isWallFollow = false;

    // Start is called before the first frame update
    void Start()
    {
        _door = door.GetComponent<Door>();
        _doorTrigger = doorTrigger.GetComponent<CapsuleCollider2D>();
        if(_roomba == null){
            _roomba = GameObject.FindGameObjectWithTag("roomba").GetComponent<Roomba>();
        }
    }

    void Update()
    {
        isWallFollow = (_roomba.GetPathType() == PathType.WallFollow);
        if(isWallFollow){
            _doorTrigger.enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        } else {
            _doorTrigger.enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roomba" && !isWallFollow && !_door.isClosed)

            {
                List<GameObject> walls = _door.AllWalls();
                foreach(GameObject wall in walls)
                {
                    Debug.Log("Remove walls");
                    wall.GetComponent<EdgeCollider2D>().enabled = false;
                    //This causes the roomba to not be touching anything and it will turn clockwise
                }
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("North") || other.CompareTag("South") ||
            other.CompareTag("West") || other.CompareTag("East"))
        {
            foreach (GameObject wall in _door.touchingWalls)
            {
                if (wall.transform.parent.Equals(other.gameObject.transform.parent))
                {
                    return;
                }
            }

            _door.touchingWalls.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(_roomba.IsTimerStarted()){
            if(other.tag == "roomba" && !isWallFollow  && !_door.isClosed)
            {
                List<GameObject> walls = _door.AllWalls();
                foreach(GameObject wall in walls)
                {
                    Debug.Log("Enable walls");
                    wall.GetComponent<EdgeCollider2D>().enabled = true;
                }
            }
        }

        if (other.CompareTag("North") || other.CompareTag("South") ||
            other.CompareTag("West") || other.CompareTag("East"))
        {
            _door.touchingWalls.Remove(other.gameObject);
        }

    }
}
