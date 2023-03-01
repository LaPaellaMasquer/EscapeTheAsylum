using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject obj;

    public GameObject lamp;

    public Material mat_On;
    public Material mat_Off;

    // outputs depends on the shape of the piece,index indicates the orientation: 0 = up, 1 = right, 2 = down, 3 = left;
    private bool[] output = new bool[4];

    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            setOn();
        }
        else
        {
            setOff();
        }
    }

    private void setOn()
    {
        changeMaterial(mat_On);
    }

    private void setOff()
    {
        changeMaterial(mat_Off);
    }

    public void rotate()
    {
        outputShift(output);
    }

    private void outputShift(bool* tab)
    {
        bool temp;
        temp = tab[0];
        tab[0] = tab[1];
        tab[1] = tab[2];
        tab[2] = tab[3];
        tab[3] = temp;
    }
    {

    }

    private void changeMaterial(Material m)
    {
        Material[] mats = lamp.GetComponent<Renderer>().materials;
        mats[0] = m;
        lamp.GetComponent<Renderer>().materials = mats;
    }

}
