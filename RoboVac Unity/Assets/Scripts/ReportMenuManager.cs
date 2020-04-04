using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportMenuManager : MonoBehaviour
{
    public GameObject menu;
    public DataCells data;

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

        // These return bools so maybe we can throw some kind of internal error if they need it or
        // populate the return upward so that whatever is calling this func can handle trying
        // to add too much data.
        data.AddRow("34", "04-04-2020", "02:35:23", "Wall Follow", "67%");
        data.AddRow("12", "04-05-2020", "03:59:42", "Snaking", "78%");
        data.AddRow("87", "04-06-2021", "12:45:54", "Random", "45%");
        data.AddRow("18", "01-19-2022", "07:56:12", "Spiral", "99%");
        data.AddRow("2", "12-12-3000", "11:28:46", "Wall Follow", "100%");

        data.AddRow("123", "12-31-2020", "12:59:59", "Random", "69%");

    }

    public void Close()
    {
        Destroy(menu);
    }
}
