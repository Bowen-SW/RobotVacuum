using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public static class UserInputInformation
{
	private static string fileName;

    public static string FileName
    {
        get 
        {
            return fileName;
        }
        set 
        {
            fileName = value;
        }
    }

}
