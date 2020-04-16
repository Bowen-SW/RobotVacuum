using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ReportMenuManager : MonoBehaviour
{
    public GameObject menu;
    public DataCells data;
    public CoverageColorChanger colorChanger;
    public TMPro.TextMeshProUGUI floorPlanNameLabel;

    // Start is called before the first frame update
    void Start()
    {
        PopulateData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopulateData()
    {

        if (UserInputInformation.saveDataGS == null)
        {
            Debug.Log("howdy");
        }

        if (UserInputInformation.saveDataGS != null)
        {
            floorPlanNameLabel.text = UserInputInformation.FileNameGS;

            int count = UserInputInformation.saveDataGS["reports"].Count;

            if (count > 0)
            {
                
                for (int i = count -1; i >= 0 && i > count - 6; i--)
                {
                    string runNumber = UserInputInformation.saveDataGS["reports"][i]["reportID"].ToString();
                    string date = UserInputInformation.saveDataGS["reports"][i]["dateStamp"].ToString();
                    string duration = UserInputInformation.saveDataGS["reports"][i]["duration"].ToString();
                    string pathType = UserInputInformation.saveDataGS["reports"][i]["algorithmType"].ToString();
                    string coverage = UserInputInformation.saveDataGS["reports"][i]["coverageValue"].ToString() + "%";

                    data.AddRow(runNumber, date, duration, pathType, coverage);
                }

                colorChanger.ChangeColorBasedOnVal();
            }
        }

    }

    public void Close()
    {
        Destroy(menu);
    }
}
