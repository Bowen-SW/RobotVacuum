using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class CoverageColorChanger : MonoBehaviour
{
    // The list of the cells of the gridlayout to change the color of
    public List<TextMeshProUGUI> labelList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorBasedOnVal()
    {
        // Look at each value and see if it falls in the right bound
        foreach (TextMeshProUGUI label in labelList)
        {
            // Parse the value to an integer ignoring any non digits for units or etc.
            int val = int.Parse(Regex.Replace(label.text, "[^.0-9]", ""));

            if (val >= 80)
            {
                Debug.Log("Green");
                label.color = new Color32(0, 255, 0, 255);
            }
            else if (val >= 50 && val <= 79)
            {
                Debug.Log("Yellow");
                label.color = new Color32(255, 255, 0, 255);
            }
            else if (val <= 49)
            {
                Debug.Log("Red");
                label.color = new Color32(255, 0, 0, 255);
            }
        }
    }

}
