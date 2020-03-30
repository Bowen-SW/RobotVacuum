using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewFloorPlanMenuManager : MonoBehaviour
{
    public GameObject menu;

    public TextMeshProUGUI fileName;
    public TextMeshProUGUI roomWidth;
    public TextMeshProUGUI roomHeight;
    public TextMeshProUGUI carpetType;

    public TestSimScript simScript;

    // Start is called before the first frame update
    void Start()
    {
        simScript = GetComponentInParent<TestSimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData()
    {
        //Debug.Log(fileName.text);
        //Debug.Log(roomWidth.text);
        //Debug.Log(roomHeight.text);
        //Debug.Log(carpetType.text);

    }

    public void Close()
    {
        // simScript.SaySomething(fileName.text);

        Destroy(menu);
    }
}
