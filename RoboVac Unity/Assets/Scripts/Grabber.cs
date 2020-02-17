using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
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
    public grabPos position;

    public GameObject parent;
    private GrabberPosition grabberPosition;

    private bool dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        switch(position)
        {
            case grabPos.TopLeft:
                grabberPosition = new TopLeft();
                break;
            case grabPos.Top:
                grabberPosition = new Top();
                break;
            case grabPos.TopRight:
                grabberPosition = new TopRight();
                break;
            case grabPos.Left:
                grabberPosition = new Left();
                break;
            case grabPos.Center:
                grabberPosition = new Center();
                break;
            case grabPos.Right:
                grabberPosition = new Right();
                break;
            case grabPos.BottomLeft:
                grabberPosition = new BottomLeft();
                break;
            case grabPos.Bottom:
                grabberPosition = new Bottom();
                break;
            case grabPos.BottomRight:
                grabberPosition = new BottomRight();
                break;

        }
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dragging)
        {
            ResetPosition();
        } else {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = target;
            grabberPosition.SetParent(parent.GetComponent<IResizable>(), transform.position);
        }
    }

    void OnMouseDown()
    {
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void ResetPosition()
    {
        transform.position = grabberPosition.ResetPosition(parent.GetComponent<IResizable>());
    }
}
