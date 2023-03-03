using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedScript : MonoBehaviour
{
    public GameObject lamp;

    public Material On;
    public Material Off;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOn(int index)
    {
        changeMaterial(On, index);
    }

    public void setOff(int index)
    {
        changeMaterial(Off, index);
    }

    void changeMaterial(Material m, int index)
    {
        Material[] mats = lamp.GetComponent<Renderer>().materials;
        mats[index] = m;
        lamp.GetComponent<Renderer>().materials = mats;
    }
}
