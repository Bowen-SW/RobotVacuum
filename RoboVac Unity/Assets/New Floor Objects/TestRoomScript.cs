using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomScript : MonoBehaviour
{
    //private List<List<float>> floorDirtiness;

    private float[,] floorDirtiness;

    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitForSimulationStart()
    {
        floorDirtiness = new float[width, height];

        /*for (int i = floorDirtiness.GetLowerBound(0); i <= floorDirtiness.GetUpperBound(0); i++)
        {
            for (int j = floorDirtiness.GetLowerBound(1); j <= floorDirtiness.GetUpperBound(1); j++)
            {
                floorDirtiness[i, j] = 1.0f;
            }
        }*/

        for (int i = 0; i < floorDirtiness.GetLength(0); i++)
        {
            for (int j = 0; j < floorDirtiness.GetLength(1); j++)
            {
                floorDirtiness[i, j] = 1.0f;
            }
        }

        Debug.Log("Initalized");

    }

    public void ClearForSimulationEnd()
    {
        for (int i = floorDirtiness.GetLowerBound(0); i <= floorDirtiness.GetUpperBound(0); i++)
        {
            for(int j = floorDirtiness.GetLowerBound(1); j <= floorDirtiness.GetUpperBound(1); j++)
            {
                Debug.Log("i: " + i.ToString() + " j: " + j.ToString() + " " + floorDirtiness[i, j].ToString());
            }
        }

        floorDirtiness = null;
    }

    public void CleanAtPosition(int x, int y, float cleaningFactor)
    {
        if (floorDirtiness != null)
        {
            float prev = floorDirtiness[x, y];

            floorDirtiness[x, y] *= cleaningFactor;

            //Debug.Log("Cleaned x: " + x.ToString() + " y: " + y.ToString() + " percentage before: " + prev.ToString() + " after: " + floorDirtiness[x,y].ToString());

        }
    }

    public float GetCleanlinessPercentage()
    {
        // TODO: Determine how to calculate the cleanliness of a room
        return 0.0f;
    }

    public void SaySomthing()
    {
        Debug.Log("In This Room!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Room Trigger collision");
    }

    public Vector2 GetCleaningOrigin()
    {
        float centerX = transform.position.x;
        float centerY = transform.position.y;

        return new Vector2(centerX - (width / 2), centerY - (height / 2));

    }

}
