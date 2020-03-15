using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    private static GameObject _selected;
    public static GameObject selected
    {
        get
        {
            return _selected;
        }

        set
        {
            Select(value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Select(GameObject o)
    {
        Selectable s;
        if(_selected != null)
        {
            s = _selected.GetComponent<Selectable>();
            s.isSelected = false;
        }
        _selected = o;
        s = o.GetComponent<Selectable>();
        if(s != null)
        {
            s.isSelected = true;
        }
    }

    public static void DeleteSelected()
    {
        if(selected == null) return;
        Destroy(selected);
        selected = null;
    }
}
