using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IMovable
{

    [SerializeField] private GameObject currentWall = null;

    private static int range = 10;
    public Vector2 target;
    public bool moving;
    public bool isRotated;
    private bool loaded = false;
    public List<GameObject> allWalls;
    private int DoorID;

    public int id
    {
        get
        {
            return DoorID;
        }
        set
        {
            DoorID = value;
            Debug.Log("Setting doorID for this room to: " + DoorID);
            UserInputInformation.AddStartVectorD(DoorID, target);
        }
    }

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

    public void LoadPositions(Vector2 start, bool rotated)
    {
        if(rotated == true)
        {
            Debug.Log("is rotated!");
            transform.rotation = Quaternion.Euler(0,0,-90);
            Debug.Log(transform.rotation);
        }
        this.target = start;
        loaded = true;
    }
    
    void Start()
    {
        if(!loaded)
        {
            int pos1, pos2;
            do {
                pos1 = Random.Range(-range, range);
                pos2 = Random.Range(-range, range);
            }while(Physics.CheckSphere(new Vector3(pos1, pos2, transform.position.z),2f));

            range ++;
            target = new Vector2((float)pos1, (float)pos2);
        }
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
                    case "South":
                        transform.rotation = Quaternion.Euler(0,0,0);
                        transform.position = new Vector3(Mathf.Round(target.x)-0.5f,transform.position.y,0f);
                        isRotated = false;
                        break;
                    case "East":
                    case "West":
                        transform.rotation = Quaternion.Euler(0,0,-90);
                        transform.position = new Vector3(transform.position.x,Mathf.Round(target.y)-0.5f,0f);
                        isRotated = true;
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
