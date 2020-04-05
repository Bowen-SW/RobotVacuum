using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void SetTarget(Vector2 position);
    Vector2 GetTarget();

    void SetMoving(bool moving);
    bool IsMoving();
}
