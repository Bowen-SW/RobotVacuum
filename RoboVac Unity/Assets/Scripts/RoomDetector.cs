using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour
{
    public Roomba roomba;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            roomba.isInRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            roomba.isInRoom = false;
        }
    }

}
