using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public enum legPos
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    };
    public legPos position;

    private IResizable p;
    public GameObject parent;

    void Start() {
        if(parent != null && p == null)
        {
            p = parent.GetComponent<IResizable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = this.transform.position;
        switch(position)
        {
            case legPos.TopLeft:
                temp.x = p.GetStart().x - 0.35f;
                temp.y = p.GetStop().y - 0.65f;
                break;
            case legPos.TopRight:
                temp.x = p.GetStop().x - 0.65f;
                temp.y = p.GetStop().y - 0.65f;
                break;
            case legPos.BottomLeft:
                temp.x = p.GetStart().x - 0.35f;
                temp.y = p.GetStart().y - 0.35f;
                break;
            case legPos.BottomRight:
                temp.x = p.GetStop().x - 0.65f;
                temp.y = p.GetStart().y - 0.35f;
                break;
        }

        this.transform.position = temp;
    }
}
