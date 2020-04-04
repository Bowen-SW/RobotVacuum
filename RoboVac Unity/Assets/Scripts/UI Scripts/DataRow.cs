using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataRow : MonoBehaviour
{
    public TextMeshProUGUI[] cells;

    private string runNumber;
    private string date;
    private string duration;
    private string pathType;
    private string coverage;

    public void setValues(string runNumber, string date, string duration, string pathType, string coverage)
    {
        setRunNumber(runNumber);
        setDate(date);
        setDuration(duration);
        setPathType(pathType);
        setCoverage(coverage);
    }

    public void setRunNumber(string runNumber)
    {
        this.runNumber = runNumber;
        cells[0].text = this.runNumber;
    }

    public void setDate(string date)
    {
        this.date = date;
        cells[1].text = this.date;
    }

    public void setDuration(string duration)
    {
        this.duration = duration;
        cells[2].text = this.duration;
    }

    public void setPathType(string pathType)
    {
        this.pathType = pathType;
        cells[3].text = this.pathType;
    }

    public void setCoverage(string coverage)
    {
        this.coverage = coverage;
        cells[4].text = this.coverage;
    }
    

}
