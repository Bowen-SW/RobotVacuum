using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Path : MonoBehaviour
{
    protected float velocity;

    protected float simSpeed;

    protected Rigidbody2D vacuum;

    public abstract void Move();

    public void SetFields (float velocity, float simSpeed, Rigidbody2D vacuum){ 
        this.velocity = velocity;
        this.simSpeed = simSpeed;
        this.vacuum = vacuum;
    }

    protected void Stop(){
        Vector3 direction = vacuum.velocity;
        float speed = 0.0F;
        vacuum.velocity = direction * speed;
    }

    
}
