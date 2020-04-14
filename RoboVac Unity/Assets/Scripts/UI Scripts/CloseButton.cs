using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject saveFileMessageBox;
    public Canvas canvas;

    public void OnClick()
    {
        if (GameObject.FindGameObjectWithTag("MessageBox") == null)
        {
            GameObject newObj = Instantiate(saveFileMessageBox, new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
            newObj.transform.SetParent(canvas.transform, false);
            RectTransform objRectTrans = newObj.GetComponent<RectTransform>();
            objRectTrans.offsetMin = new Vector2(0.0f, 0.0f);
            objRectTrans.offsetMax = new Vector2(0.0f, 0.0f);
        }
    }

}
