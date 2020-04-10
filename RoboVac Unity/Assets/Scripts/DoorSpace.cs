using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpace : MonoBehaviour
{
    public GameObject door;
    private Door _door;

    // Start is called before the first frame update
    void Start()
    {
        _door = door.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "roomba")
        {
            List<GameObject> walls = _door.AllWalls();
            foreach(GameObject wall in walls)
            {
                wall.GetComponent<EdgeCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "roomba")
        {
            List<GameObject> walls = _door.AllWalls();
            foreach(GameObject wall in walls)
            {
                wall.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }
    }
}
