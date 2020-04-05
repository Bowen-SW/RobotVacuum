using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCells : MonoBehaviour
{
    public DataRow[] dataRows;
    private int rowIndex = 0;

    public bool AddRow(string runNumber, string date, string duration, string pathType, string coverage)
    {
        if (rowIndex <= dataRows.Length - 1)
        {
            dataRows[rowIndex].setValues(runNumber, date, duration, pathType, coverage);
            rowIndex++;
        }
        else
        {
            return false;
        }

        return true;
    }

}
