using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoombaScript : MonoBehaviour
{
    public TestRoomScript testRoom;
    Vector2 currentRoomOrigin;

    public GameObject vaccum;

    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (testRoom != null && isRunning)
        {
            // TODO: Have x and y of room object origin (bottom left)

            // TODO: Get position of roomba relative to room origin (roomba position - room origin)
            float compX = vaccum.transform.position.x - currentRoomOrigin.x;
            float compY = vaccum.transform.position.y - currentRoomOrigin.y;

            int x = Mathf.RoundToInt(compX);
            int y = Mathf.RoundToInt(compY);

            testRoom.CleanAtPosition(x, y, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TestRoomScript t = collision.gameObject.GetComponentInParent<TestRoomScript>();

        if (t != null)
        {
            testRoom = t;
            currentRoomOrigin = testRoom.GetCleaningOrigin();
        }
    }

    public void ToggleRunning()
    {
        if (isRunning)
        {
            isRunning = false;
            testRoom.ClearForSimulationEnd();
        }
        else
        {
            testRoom.InitForSimulationStart();
            isRunning = true;
        }
    }
}
