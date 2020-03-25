using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            (p.GetStart().x + p.GetStop().x) / 2f,
            (p.GetStart().y + p.GetStop().y) / 2f)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        Vector2 wh2 = (p.GetStart() - p.GetStop()) / new Vector2(2.0f,2.0f);
        p.SetStart(pos - wh2);
        p.SetStop(pos + wh2);
    }
}
