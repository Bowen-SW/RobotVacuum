using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileManager : MonoBehaviour
{
    // Opens Windows File Explorer
    public void OpenFileExplorer()
    {
        // Get the path to the file that the user specifies in File Explorer
        string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", "", "json");
    }

}
