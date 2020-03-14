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
        // Adds the item this button is set up for to the scene at the origin with no rotation modifiers
        Instantiate(objectToAdd, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

}
