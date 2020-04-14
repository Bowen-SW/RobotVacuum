using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject messageBox;

    // Called when the Confirm button is pressed
    public void Accept()
    {
        // TODO: Add your code here
        Close();
    }

    // Called when the Cancel button is pressed
    public void Close()
    {
        Destroy(messageBox);
    }

}
