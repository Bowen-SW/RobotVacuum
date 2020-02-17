using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeft : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            p.GetStart().x,
            p.GetStop().y)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetStart(new Vector2(pos.x,p.GetStart().y));
        p.SetStop(new Vector2(p.GetStop().x,pos.y));
    }
}
