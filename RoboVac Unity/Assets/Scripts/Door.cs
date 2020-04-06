using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IMovable
{

    [SerializeField] private GameObject currentWall = null;

    public Vector2 target;
    public bool moving;

    public List<GameObject> allWalls;

    public void SetTarget(Vector2 position)
    {
        target = position;
    }

    public Vector2 GetTarget()
    {
        return target;
    }

    public void SetMoving(bool moving)
    {
        this.moving = moving;
    }

    public bool IsMoving()
    {
        return this.moving;
    }

    // Start is called before the first frame update
    void Start()
    {
        target.x = transform.position.x;
        target.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(allWalls.Count == 0 || currentWall == null)
        {
            allWalls = AllWalls();
        }
        foreach(GameObject wall in allWalls)
        {
            if(wall == null)
            {
                allWalls = AllWalls();
            }
        }
        

        if(!moving)
        {
            if(currentWall == null)
            {
                currentWall = NearestWall();
            }

            if(currentWall != null)
            {
                transform.position = currentWall.transform.position;
                if(currentWall.tag == "West" || currentWall.tag == "East")
                {
                    transform.rotation = Quaternion.Euler(0,0,90);
                    transform.position += new Vector3(0f,0.5f,0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0,0,0);
                    transform.position += new Vector3(0.5f,0f,0f);
                }
                List<GameObject> nearby = NearbyWalls();
                foreach(GameObject wall in nearby)
                {
                    if(((currentWall.tag == "East" || currentWall.tag == "West") && (wall.tag == "East" || wall.tag == "West")
                     || (currentWall.tag == "North" || currentWall.tag == "South") && (wall.tag == "North" || wall.tag == "South")))
                    {
                        wall.gameObject.SetActive(false);
                    }
                }
            }

        }
        else
        {
            if(currentWall != null)
            {
                List<GameObject> nearby = NearbyWalls();
                foreach(GameObject wall in nearby)
                {
                    wall.gameObject.SetActive(true);
                }
                currentWall = null;
            }

            if(currentWall == null)
            {
                transform.position = target;
            }
        }
    }

    GameObject NearestWall()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach(GameObject wall in allWalls)
        {
            float curDist = (wall.transform.position - transform.position).sqrMagnitude;
            if(curDist < distance)
            {
                distance = curDist;
                closest = wall;
            }
        }
        return closest;
    }

    List<GameObject> NearbyWalls()
    {
        List<GameObject> close = new List<GameObject>();
        foreach(GameObject wall in allWalls)
        {
            float curDist = (wall.transform.position - transform.position).sqrMagnitude;
            if(curDist <= 1.0f)
            {
                close.Add(wall);
            }
        }
        return close;
    }

    List<GameObject> AllWalls()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("North"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("West"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("South"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("East"));
        return gameObjects;
    }
}
