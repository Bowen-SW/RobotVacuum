using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection 
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

    public static void Select(GameObject o)
    {
        Selectable s;
        if(_selected != null)
        {
            s = _selected.GetComponent<Selectable>();
            s.isSelected = false;
        }
        _selected = o;
        if(o != null)
        {
            s = o.GetComponent<Selectable>();
            if(s != null)
            {
                s.isSelected = true;
            }
        }
    }

    public static void DeleteSelected()
    {
        if(selected == null || selected.GetComponentInChildren<Roomba>()) return;
        Object.Destroy(selected);
        selected = null;
    }
}
