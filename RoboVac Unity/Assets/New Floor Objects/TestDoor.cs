using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public List<GameObject> connectedFloors;

    public GameObject wall;

    public bool isBeingMoved = false;

    public Collider2D wallCollider;

    public Collider2D floorCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreCollision(floorCollider, GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            float newY = transform.position.y - 0.1f;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }*/

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float newX = transform.position.x + 0.1f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Object name: " + collision.gameObject.name.ToString());


        /*if (collision.gameObject.CompareTag("floor"))
        {
            Debug.Log("Found a floor");
            connectedFloors.Add(collision.gameObject);
        }


        // Disable collisions with the wall this door is attached to and the roomba
        if (collision.gameObject.CompareTag("roomba"))
        {

            Physics2D.IgnoreCollision(wallCollider, collision.gameObject.GetComponent<Collider2D>());
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("floor"))
        {
            connectedFloors.Remove(collision.gameObject);
        }

        // Enabled previously disabled collisions since roomba is outside of the door
        if (collision.gameObject.CompareTag("roomba"))
        {
            Physics2D.IgnoreCollision(wallCollider, collision.gameObject.GetComponent<Collider2D>(), false);
        }*/
    }

    

}
