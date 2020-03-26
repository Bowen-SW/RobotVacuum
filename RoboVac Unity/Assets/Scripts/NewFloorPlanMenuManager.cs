using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class NewFloorPlanMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputField nameField;
    public TMP_InputField width;
    public TMP_InputField height;
    static string fileName;
    // Start is called before the first frame update
    void Start()
    {
        nameField.text = "FloorPlan1";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        if(nameField.text!="")
        {
            Debug.Log(nameField.text);
            fileName = nameField.text;
            UserInputInformation.FileName = fileName;
            
        }
        SceneManager.LoadScene(1);
        Debug.Log(UserInputInformation.FileName);
    }

    public void printFileName()
    {
        Debug.Log(nameField.text);
    }
}
