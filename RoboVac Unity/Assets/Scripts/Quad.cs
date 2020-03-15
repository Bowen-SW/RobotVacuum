using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{

    private float dirtiness = 1.0f;
    public float coverage
    {
        get
        {
            return dirtiness;
        }

        set {}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, dirtiness);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "whiskers")
        {
            // dirtiness *= 1.0f - whisker_efficiency;
        }
        else if(other.gameObject.tag == "vacuum")
        {
            // dirtiness *= 1.0f - vacuum_efficiency;
        }
    }
}
