using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{

    public GameObject menu;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSettingsMenu()
    {
        if (GameObject.FindGameObjectWithTag("SettingsMenu") == null)
        {
            GameObject newObj = Instantiate(menu, new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            newObj.transform.SetParent(canvas.transform, false);
            RectTransform objRectTrans = newObj.GetComponent<RectTransform>();
            objRectTrans.offsetMin = new Vector2(0.0f, 0.0f);
            objRectTrans.offsetMax = new Vector2(0.0f, 0.0f);
        }
    }

}
