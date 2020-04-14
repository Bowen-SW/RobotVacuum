using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IResizable
{
    private static int range = 10;
    public Vector2 start;
    public Vector2 stop;
    private int ChestID;
    private bool loaded;

    public int id
    {
        get
        {
            return ChestID;
        }
        set
        {
            ChestID = value;
            Debug.Log("Setting chestID for this room to: " + ChestID);
            UserInputInformation.AddStartVectorCT(ChestID, start);
            UserInputInformation.AddStopVectorCT(ChestID, stop);
        }
    }
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
    
    public void LoadPositions(Vector2 start, Vector2 stop)
    {
        loaded = true;
        this.start = start;
        this.stop = stop;
        
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
        if(this.width < 1.0f || this.height < 1.0f)
        {
            this.start = temp;
        }
        return start;
    }

    public Vector2 SetStop(Vector2 position)
    {
        Vector2 temp = this.stop;
        this.stop.x = Mathf.Round(position.x + 0.5f);
        this.stop.y = Mathf.Round(position.y + 0.5f);
        if(this.width < 1.0f || this.height < 1.0f)
        {
            this.stop = temp;
        }
        return stop;
    }

    public Vector2 SetLeft(float position)
    {
        SetStart(new Vector2(position, this.start.y-0.5f));
        return start;
    }

    public Vector2 SetRight(float position)
    {
        SetStop(new Vector2(position, this.stop.y-0.5f));
        return stop;
    }

    public Vector2 SetTop(float position)
    {
        SetStop(new Vector2(this.stop.x-0.5f, position));
        return stop;
    }

    public Vector2 SetBottom(float position)
    {
        SetStart(new Vector2(this.start.x-0.5f, position));
        return start;
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
            start = new Vector2((float)pos1, (float)pos2);
            stop = new Vector2((float) pos1+2, (float)pos2+2);
        }
    }
    void Update() {
        this.transform.position = new Vector3(((this.start.x + this.stop.x) / 2f - 0.5f), ((this.start.y + this.stop.y) / 2f - 0.5f), this.transform.position.z);
        this.transform.localScale = new Vector3(this.width, this.height, 1.0f);
    }
}