using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room : MonoBehaviour, IResizable
{

    private static int range = 10;
    public Material[] floorTypes;

    public Vector2 start;
    private Vector2 prevStart = new Vector2(-1000,-1000);
    public Vector2 stop;
    private Vector2 prevStop = new Vector2(-1000,-1000);
    private int RoomID;

    private bool loaded = false;

    private float[,] cells;

    
    public int id
    {
        get
        {
            return RoomID;
        }
        set
        {
            RoomID = value;
            Debug.Log("Setting roomID for this room to: " + RoomID);
            UserInputInformation.AddStartVector(RoomID, start);
            UserInputInformation.AddStopVector(RoomID, stop);
        }
    }


    public int width
    {
        get
        {
            UserInputInformation.RoomWidthGS = ((int)(this.stop.x - this.start.x));
            return (int)(this.stop.x - this.start.x);
        }
        private set {}
    }

    public int height
    {
        get
        {
            UserInputInformation.RoomHeightGS = ((int)(this.stop.y - this.start.y));
            return (int)(this.stop.y - this.start.y);
        }
        private set {}
    }

    public int sqft
    {
        get
        {
            return width * height;
        }
        set {}
    }

    public Vector2 GetStart()
    {
        return this.start;
    }

    public Vector2 GetStop()
    {
        return this.stop;
    }

    public Vector2 SetStart(Vector2 position)
    {
        Vector2 temp = this.start;
        this.start.x = Mathf.Round(position.x + 0.5f);
        this.start.y = Mathf.Round(position.y + 0.5f);
        if(this.sqft < 4)
        {
            this.start = temp;
        }
        //Debug.Log("Using Room ID: " + RoomID);
        UserInputInformation.AddStartVector(RoomID, start);
        return start;
    }

    public Vector2 SetStop(Vector2 position)
    {
        Vector2 temp = this.stop;
        this.stop.x = Mathf.Round(position.x + 0.5f);
        this.stop.y = Mathf.Round(position.y + 0.5f);
        if(this.sqft < 4)
        {
            this.stop = temp;
        }
        UserInputInformation.AddStopVector(RoomID, stop);
        return stop;
    }

    public Vector2 SetLeft(float position)
    {
        return this.SetStart(new Vector2(position, this.prevStart.y-0.5f));
    }

    public Vector2 SetRight(float position)
    {
        return this.SetStop(new Vector2(position, this.prevStop.y-0.5f));
    }

    public Vector2 SetTop(float position)
    {
        return this.SetStop(new Vector2(this.prevStop.x-0.5f, position));
    }

    public Vector2 SetBottom(float position)
    {
        return this.SetStart(new Vector2(this.prevStart.x-0.5f, position));
    }

    public void LoadPositions(Vector2 start, Vector2 stop)
    {
        this.start = start;
        this.stop = stop;
        loaded = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetFloor().GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        if(!loaded)
        {
            int pos1, pos2;
            do {
                pos1 = Random.Range(-range, range);
                pos2 = Random.Range(-range, range);
            }while(Physics.CheckSphere(new Vector3(pos1, pos2, transform.position.z),2f));

            range ++;
            start = new Vector2((float)pos1, (float)pos2);
            stop = new Vector2((float) pos1+2, (float)pos2+2);
        }

        ResetCells();       
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(this.start.x - this.prevStart.x) >= 0.2 ||
           Mathf.Abs(this.start.y - this.prevStart.y) >= 0.2 ||
           Mathf.Abs(this.stop.x - this.prevStop.x) >= 0.2 ||
           Mathf.Abs(this.stop.y - this.prevStop.y) >= 0.2)
        {
            CreateRoom();
        }

        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100);

            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 1.0f);
            
            if(hit.collider != null)
            {
                if(hit.transform.parent == transform)
                {
                    Selection.Select(this.gameObject);
                }
            }
            else if(hit2D.collider != null)
            {
                if(hit2D.transform.parent == transform)
                {
                    Selection.Select(this.gameObject);
                }
            }
        }

        if(Object.FindObjectOfType<Simulation>().IsStopped())
        {
            if(UserInputInformation.clearCoverageGS==true)
            {
                ResetCells();
            }
        }

        GameObject floor = GetFloor();
        floor.GetComponent<MeshRenderer>().material = floorTypes[(int)(Object.FindObjectOfType<RoombaSettingsScript>().GetFloorType())];
        floor.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    private void CreateRoom()
    {
        cells = new float[width,height];
        List<GameObject> walls = GetWalls();
        GameObject floor = GetFloor();
        foreach(GameObject wall in walls)
        {
            switch(wall.tag)
            {
                case "North":
                    wall.transform.position = new Vector3((this.start.x + this.stop.x)/2 - 0.5f, this.stop.y - 0.5f, 0.05f);
                    wall.transform.localScale = new Vector3(this.width-0.2f, 0.2f, transform.position.z);
                    break;
                case "East":
                    wall.transform.position = new Vector3(this.stop.x - 0.5f, (this.start.y + this.stop.y)/2 - 0.5f, 0.05f);
                    wall.transform.localScale = new Vector3(0.2f, this.height-0.2f, transform.position.z);
                    break;
                case "South":
                    wall.transform.position = new Vector3((this.start.x + this.stop.x)/2 - 0.5f, this.start.y - 0.5f, 0.05f);
                    wall.transform.localScale = new Vector3(this.width-0.2f, 0.2f, transform.position.z);
                    break;
                case "West":
                    wall.transform.position = new Vector3(this.start.x - 0.5f, (this.start.y + this.stop.y)/2 - 0.5f, 0.05f);
                    wall.transform.localScale = new Vector3(0.2f, this.height-0.2f, transform.position.z);
                    break;
            }
        }
        
        floor.transform.position = new Vector3(this.start.x + this.width/2f - 0.5f,this.start.y + this.height/2f - 0.5f, 0.6f);
        floor.transform.localScale = new Vector3(this.width-0.2f, this.height-0.2f, 1);

        this.prevStart.x = this.start.x;
        this.prevStart.y = this.start.y;
        this.prevStop.x = this.stop.x;
        this.prevStop.y = this.stop.y;
        UserInputInformation.AddStartVector(RoomID, start);
        UserInputInformation.AddStopVector(RoomID, stop);
        ResetCells();
    }

    private List<GameObject> GetWalls()
    {
        List<GameObject> walls = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject wall = transform.GetChild(i).gameObject;
            if(wall.tag == "North" || wall.tag == "East" || wall.tag == "South" || wall.tag == "West")
            {
                walls.Add(wall);
            }
        }
        return walls;
    }

    private GameObject GetFloor()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject floor = transform.GetChild(i).gameObject;
            if(floor.tag == "floor")
            {
                return floor;
            }
        }
        return null;
    }

    public float getCoverage()
    {
        float total = 0.0f;
        foreach(float c in cells)
        {
            total += 1.0f - c;
        }
        return total;
    }

    public float GetCoveragePercent()
    {
        Debug.Log(getCoverage() / sqft);
        return getCoverage() / sqft;
    }

    public void VacuumCell(Roomba roomba)
    {
        try{
            Vector2 pos = new Vector2(roomba.transform.GetChild(1).transform.position.x, roomba.transform.GetChild(1).transform.position.y);
            cells[Mathf.RoundToInt(pos.x - this.start.x), Mathf.RoundToInt(pos.y - this.start.y)] *= 1.0f - (roomba.GetVacEff() * Time.deltaTime / 100.0f);
        } catch { }
    }

    public void WhiskerCell(Roomba roomba)
    {
        try{
            Vector2 pos = new Vector2(roomba.transform.GetChild(2).transform.position.x, roomba.transform.GetChild(2).transform.position.y);
            cells[Mathf.RoundToInt(pos.x - this.start.x), Mathf.RoundToInt(pos.y - this.start.y)] *= 1.0f - (roomba.GetWhiskerEff() * Time.deltaTime / 100.0f);
        } catch { }
    }

    void ResetCells()
    {
        cells = new float[width, height];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                cells[i,j] = 1.0f;
            }
        }
    }

}
