using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    public float velocity;

    public float simSpeed;

    public Rigidbody2D vacuum;

    public abstract void Move();
}
