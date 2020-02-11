using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Roomba roomba;
    void Start()
    {
        Debug.Log("GameHandler.Start");

        roomba.Launch();
    }
}
