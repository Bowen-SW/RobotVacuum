using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRight : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            p.GetStop().x,
            p.GetStart().y)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetStart(new Vector2(p.GetStart().x,pos.y));
        p.SetStop(new Vector2(pos.x,p.GetStop().y));
    }
}
