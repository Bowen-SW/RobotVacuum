using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public GameObject parent;

    private Selectable parentSelect;

    private IMovable parentMove;

    private bool dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = parent.transform.position;
        this.parentSelect = parent.GetComponent<Selectable>();
        this.parentMove = parent.GetComponent<IMovable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dragging || !parentSelect.isSelected)
        {
            transform.position = parent.transform.position;
            parentMove.SetMoving(false);
        } else {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = target;
            parentMove.SetTarget(target);
            parentMove.SetMoving(true);
        }

        if(parentSelect.isSelected && !gameObject.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }else if(!parentSelect.isSelected && gameObject.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
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
}
