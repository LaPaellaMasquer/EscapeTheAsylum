using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject lamp;

    public Material mat_On;
    public Material mat_Off;

    public bool up, down, right, left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOn()
    {
        changeMaterial(mat_On);
    }

    public void setOff()
    {
        changeMaterial(mat_Off);
    }

    void changeMaterial(Material m)
    {
        Material[] mats = lamp.GetComponent<Renderer>().materials;
        mats[0] = m;
        lamp.GetComponent<Renderer>().materials = mats;
    }

}
