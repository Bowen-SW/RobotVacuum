using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GrabberPosition
{
    Vector2 ResetPosition(IResizable parent);
    void SetParent(IResizable parent, Vector2 position);
}
