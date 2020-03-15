using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResizable
{
    Vector2 GetStart();
    Vector2 GetStop();
    Vector2 SetStart(Vector2 position);
    Vector2 SetStop(Vector2 position);
    Vector2 SetLeft(float position);
    Vector2 SetRight(float position);
    Vector2 SetTop(float position);
    Vector2 SetBottom(float position);

}
