using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    public Canvas canvas;
    public GameObject noDoorsForEachRoomMessageBox;
    public Simulation sim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForErrors()
    {
        if (!AllRoomsHaveDoors())
        {
            if (GameObject.FindGameObjectWithTag("MessageBox") == null)
            {
                GameObject newObj = Instantiate(noDoorsForEachRoomMessageBox, new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                newObj.transform.SetParent(canvas.transform, false);
                RectTransform objRectTrans = newObj.GetComponent<RectTransform>();
                objRectTrans.offsetMin = new Vector2(0.0f, 0.0f);
                objRectTrans.offsetMax = new Vector2(0.0f, 0.0f);
            }

            sim.hasErrors = true;

            return;
        }

        sim.hasErrors = false;

    }

    private bool AllRoomsHaveDoors()
    {
        Floor[] floors = Object.FindObjectsOfType<Floor>();

        if (floors.Length > 0)
        {
            foreach (Floor f in floors)
            {
                if (!f.hasDoor)
                {
                    return false;
                }
            }
        }

        return true;
    }

}
