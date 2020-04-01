using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour
{
    public Button button;
    public GameObject objectToAdd;

    // Start is called before the first frame update
    void Start()
    {
        // Setup the button with its callback
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when the button is clicked
    void TaskOnClick()
    {
        if(button.name == "Item Button 1")
        {
            Debug.Log("b1");
        }
        else if(button.name == "Item Button 2")
        {
            Debug.Log("b2");
        }
        else if(button.name == "Item Button 3")
        {
            Debug.Log("b3");
        }
        else if(button.name == "Item Button 4")
        {
            Debug.Log("b4");
        }
        else if(button.name == "Item Button 5")
        {
            UserInputInformation.AddRoom((GameObject)Instantiate(objectToAdd, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)));
        }
    }

}
