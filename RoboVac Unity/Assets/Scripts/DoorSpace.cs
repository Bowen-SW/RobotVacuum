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
        if(other.tag == "roomba" && !_door.isClosed)
        {
            List<GameObject> walls = _door.AllWalls();
            foreach(GameObject wall in walls)
            {
                wall.GetComponent<EdgeCollider2D>().enabled = false;
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
        if(other.tag == "roomba" && !_door.isClosed)
        {
            List<GameObject> walls = _door.AllWalls();
            foreach(GameObject wall in walls)
            {
                wall.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }

        if (other.CompareTag("North") || other.CompareTag("South") ||
            other.CompareTag("West") || other.CompareTag("East"))
        {
            _door.touchingWalls.Remove(other.gameObject);
        }

    }
}
