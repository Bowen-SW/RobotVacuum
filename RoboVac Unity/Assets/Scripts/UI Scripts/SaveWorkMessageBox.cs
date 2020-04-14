using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWorkMessageBox : MonoBehaviour
{
    public GameObject messageBox;

    // Called when Confirm button is clicked
    public void Accept()
    {
        // TODO: Add whatever code is needed to save the floorplan here
        SaveLoad saveLoad = GetComponentInParent<SaveLoad>();

        saveLoad.Save();

        Close();
    }

    // Called when Cancel button is clicked
    public void Close()
    {
        CloseApp();
    }

    private void CloseApp()
    {
        // Note: This will not work within the Editor. To test this, you will need a build of the application
        Application.Quit();
    }

}
