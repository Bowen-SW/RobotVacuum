using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{

    public string filePath;

    // Opens Windows File Explorer
    public void OpenFileExplorer()
    {
        // Get the path to the file that the user specifies in File Explorer
        //System.Diagnostics.Process p = new System.Diagnostics.Process();
        //p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");

        //Launcher.LaunchFileWithPicker("C:\\Users\\tjmos\\Documents\\UAH\\CS499");


        //p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe", "/select,C:\\Users\\tjmos\\Documents\\UAH\\CS499");

        //p.Start();

        //string filePath = EditorUtility.OpenFilePanel("Open a Floor Plan", "", "json");

        string initialDir = @"C:\";
        bool restoreDir = true;
        string title = "Open a Text File";
        string defExt = "txt";
        string filter = "json files (*.json)|*.json";

        SmartDLL.SmartFileExplorer fileExplorer = new SmartDLL.SmartFileExplorer();
        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);

        filePath = fileExplorer.fileName;

    }

}
