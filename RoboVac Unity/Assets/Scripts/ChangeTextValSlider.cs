using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTextValSlider : MonoBehaviour
{
    public TextMeshProUGUI valueLabel;  // The text that displays the value of the slider
    public Slider slider; // The Slider object that changes value
    public string units;    // The units to add to the end of the value string to be displayed

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the label with the default value of the slider
        ChangeValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Displays the value of the slider in the label
    public void ChangeValue()
    {
        // Take the value of the slider and add the specified units to the end for display
        valueLabel.text = slider.value.ToString() + units;
    }

}
