using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return new Vector2(
            p.GetStop().x,
            (p.GetStart().y + p.GetStop().y) / 2f)
            + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetStop(new Vector2(pos.x,p.GetStop().y));
    }
}
