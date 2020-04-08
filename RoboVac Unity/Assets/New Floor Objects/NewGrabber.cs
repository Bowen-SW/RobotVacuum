using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrabber : MonoBehaviour
{
    public enum grabPos
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    };

    public grabPos grabPosition;

    public string name;

    public Resizer resizer;

    private bool isSelected = false;

    private Vector3 previousLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {

            MoveWithMouse();
            /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);*/
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(name + " has been clicked!");

        isSelected = true;

        previousLocation = transform.position;

    }

    private void OnMouseUp()
    {
        isSelected = false;
    }

    private void MoveWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (grabPosition.Equals(grabPos.Top) || grabPosition.Equals(grabPos.Bottom))
        {
            // Move only on the y axis
            float oldY = previousLocation.y;
            float newY = mousePos.y;
            transform.position = new Vector3(transform.position.x, mousePos.y, transform.position.z);

            Debug.Log(newY - oldY);

            if (newY - oldY >= 0.5)
            {
                // Grabber is moving upwards
                if (grabPosition.Equals(grabPos.Top))
                {
                    resizer.resize(1.0f, "y", false);
                }
                else if (grabPosition.Equals(grabPos.Bottom))
                {
                    resizer.resize(-1.0f, "y", true);
                }
                previousLocation.y += 1.0f;
            }
            else if (newY - oldY <= -0.5f)
            {
                // Grabber is moving downwards
                if (grabPosition.Equals(grabPos.Top))
                {
                    resizer.resize(-1.0f, "y", false);
                }
                else if (grabPosition.Equals(grabPos.Bottom))
                {
                    resizer.resize(1.0f, "y", true);
                }
                previousLocation.y -= 1.0f;
            }


        }
        else if (grabPosition.Equals(grabPos.Left) || grabPosition.Equals(grabPos.Right))
        {
            // Move only on the x axis
            float oldX = previousLocation.x;
            float newX = mousePos.x;
            transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);

            Debug.Log(newX - oldX);

            if (newX - oldX >= 0.5)
            {
                // Grabber is moving upwards
                if (grabPosition.Equals(grabPos.Right))
                {
                    resizer.resize(1.0f, "x", false);
                }
                else if (grabPosition.Equals(grabPos.Left))
                {
                    resizer.resize(-1.0f, "x", true);
                }
                previousLocation.x += 1.0f;
            }
            else if (newX - oldX <= -0.5f)
            {
                // Grabber is moving downwards
                if (grabPosition.Equals(grabPos.Right))
                {
                    resizer.resize(-1.0f, "x", false);
                }
                else if (grabPosition.Equals(grabPos.Left))
                {
                    resizer.resize(1.0f, "x", true);
                }
                previousLocation.x -= 1.0f;
            }
        }
    }

}
