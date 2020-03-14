using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            p.GetStart().x,
            (p.GetStart().y + p.GetStop().y) / 2f)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetLeft(pos.x);
    }
}
