using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, IResizable
{
    public GameObject cell;

    public Vector2 start;
    private Vector2 prevStart = new Vector2(0,0);
    public Vector2 stop;
    private Vector2 prevStop = new Vector2(0,0);

    public int width
    {
        get
        {
            return (int)(this.stop.x - this.start.x);
        }
        private set {}
    }

    public int height
    {
        get
        {
            return (int)(this.stop.y - this.start.y);
        }
        private set {}
    }

    private GameObject[,] cells;

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
        this.start.x = Mathf.Round(position.x + 0.5f);
        this.start.y = Mathf.Round(position.y + 0.5f);
        return start;
    }

    public Vector2 SetStop(Vector2 position)
    {
        this.stop.x = Mathf.Round(position.x + 0.5f);
        this.stop.y = Mathf.Round(position.y + 0.5f);
        return stop;
    }

    public Vector2 SetLeft(float position)
    {
        this.start.x = Mathf.Round(position + 0.5f);
        return start;
    }

    public Vector2 SetRight(float position)
    {
        this.stop.x = Mathf.Round(position + 0.5f);
        return stop;
    }

    public Vector2 SetTop(float position)
    {
        this.stop.y = Mathf.Round(position + 0.5f);
        return stop;
    }

    public Vector2 SetBottom(float position)
    {
        this.start.y = Mathf.Round(position + 0.5f);
        return start;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(this.start.x - this.prevStart.x) >= 0.2 ||
           Mathf.Abs(this.start.y - this.prevStart.y) >= 0.2 ||
           Mathf.Abs(this.stop.x - this.prevStop.x) >= 0.2 ||
           Mathf.Abs(this.stop.y - this.prevStop.y) >= 0.2)
            cells = CreateRoom();
    }

    private void ClearRoom()
    {
        if(cells == null || cells.Length <= 0)
            return;
        foreach(GameObject cell in cells)
        {
            Destroy(cell);
        }
    }

    private GameObject[,] CreateRoom()
    {
        Debug.Log("Creating room...");
        ClearRoom();
        GameObject[,] newCells = new GameObject[this.width, this.height];
        for(int j = 0; j < this.height; j++)
        {
            for(int i = 0; i < this.width; i++)
            {
                newCells[i, j] = Instantiate(cell, new Vector3(this.start.x + i, this.start.y + j,0), Quaternion.identity);
                GameObject[] newCellWalls = GetWalls(newCells[i,j]);
                if(j != 0)
                    Destroy(newCellWalls[2]);
                if(j != this.height - 1)
                    Destroy(newCellWalls[0]);
                if(i != 0)
                    Destroy(newCellWalls[1]);
                if(i != this.width - 1)
                    Destroy(newCellWalls[3]);
            }
        }

        this.prevStart.x = this.start.x;
        this.prevStart.y = this.start.y;
        this.prevStop.x = this.stop.x;
        this.prevStop.y = this.stop.y;

        return newCells;
    }

    private GameObject[] GetWalls(GameObject cell)
    {
        GameObject[] walls = new GameObject[4];
        for(int i = 0; i < 4; i++)
        {
            walls[i] = cell.transform.GetChild(i+1).gameObject;
        }
        return walls;
    }
}
