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

        if(currentWall != null)
        {
            MeshRenderer floor = transform.GetChild(2).GetComponent<MeshRenderer>();
            MeshRenderer parentFloor = currentWall.transform.parent.GetChild(0).GetComponent<MeshRenderer>(); 
            floor.material = parentFloor.material;
            floor.material.color = parentFloor.material.color;
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
                switch(currentWall.tag)
                {
                    case "North":
                        transform.rotation = Quaternion.Euler(0,0,0);
                        transform.position = new Vector3(Mathf.Round(target.x)-0.5f,transform.position.y-0.1f,0f);
                        break;
                    case "East":
                        transform.rotation = Quaternion.Euler(0,0,-90);
                        transform.position = new Vector3(transform.position.x-0.1f,Mathf.Round(target.y)-0.5f,0f);
                        break;
                    case "South":
                        transform.rotation = Quaternion.Euler(0,0,180);
                        transform.position = new Vector3(Mathf.Round(target.x)-0.5f,transform.position.y+0.1f,0f);
                        break;
                    case "West":
                        transform.rotation = Quaternion.Euler(0,0,90);
                        transform.position = new Vector3(transform.position.x+0.1f,Mathf.Round(target.y)-0.5f,0f);
                        break;
                }
            }

        }
        else
        {
            transform.position = target;
            currentWall = null;
        }
    }

    public GameObject NearestWall()
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

    public List<GameObject> NearbyWalls()
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

    public List<GameObject> AllWalls()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("North"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("West"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("South"));
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("East"));
        return gameObjects;
    }
}
