using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRight : GrabberPosition
{
    public Vector2 ResetPosition(IResizable p)
    {
        return p.GetStop() + new Vector2(-0.5f,-0.5f);
    }

    public void SetParent(IResizable p, Vector2 pos)
    {
        p.SetStop(pos);
    }
}
