using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Ball ball;
    void Start()
    {
        Debug.Log("GameHandler.Start");

        ball.Launch();
    }
}
