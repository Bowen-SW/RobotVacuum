using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IResizable
{
    public Vector2 start;
    public Vector2 stop;

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

    void Update() {
        this.transform.position = new Vector3(((this.start.x + this.stop.x) / 2f - 0.5f), ((this.start.y + this.stop.y) / 2f - 0.5f), this.transform.position.z);
        this.transform.localScale = new Vector3(this.width, this.height, 1.0f);
    }
}