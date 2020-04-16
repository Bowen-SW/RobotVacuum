using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {

        Debug.Log(Selection.selected.name);

        UserInputInformation.RemoveItem(Selection.selected);

        // Delete the currently selected object
        Selection.DeleteSelected();

    }

}
