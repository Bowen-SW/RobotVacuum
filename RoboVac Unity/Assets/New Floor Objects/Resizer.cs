using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{

    //public bool inverse;
    public float resizeAmount;
    public string resizeDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            resize(resizeAmount, resizeDirection, false);
        }*/
    }

    public void resize(float amount, string direction, bool inverse)
    {
        //Debug.Log("LocalScale: " + transform.localScale.ToString());


        if (direction == "x" && inverse == false)
        {
            transform.position = new Vector3(transform.position.x + (amount / 2f), transform.position.y, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x + amount, transform.localScale.y, transform.localScale.z);
        }
        else if (direction == "y" && inverse == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (amount / 2f), transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + amount, transform.localScale.z);
        }

        if (direction == "x" && inverse == true)
        {
            transform.position = new Vector3(transform.position.x - (amount / 2f), transform.position.y, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x + amount, transform.localScale.y, transform.localScale.z);
        }
        else if (direction == "y" && inverse == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (amount / 2f), transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + amount, transform.localScale.z);
        }

    }

}
