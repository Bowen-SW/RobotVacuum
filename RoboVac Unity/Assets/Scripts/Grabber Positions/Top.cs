using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            (p.GetStart().x + p.GetStop().x) / 2f,
            p.GetStop().y)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetStop(new Vector2(p.GetStop().x,pos.y));
    }
}
