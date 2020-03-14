using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public GameObject quad;
    public List<Quad> quads = new List<Quad>();

    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0; j < 6; j++)
        {
            for(int i = 0; i < 6; i++)
            {
                GameObject q = Instantiate(quad, new Vector3(this.transform.position.x - 0.4168f + (i * 0.16672f), this.transform.position.y - 0.4168f + (j * 0.08336f), 0), Quaternion.identity);
                q.transform.parent = this.transform;
                quads.Add(q.GetComponent<Quad>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCoverage()
    {
        float total = 36.0f;
        foreach(Quad q in quads)
        {
            total -= q.coverage;
        }
        return total / 36.0f;
    }

}
