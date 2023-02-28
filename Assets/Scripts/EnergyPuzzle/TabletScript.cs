using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletScript : MonoBehaviour
{

    public GameObject lamp1;
    public GameObject lamp2;

    public GameObject tab;

    public Material On;
    public Material Off;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.batteryStatus == BatteryStatus.Charging || SystemInfo.batteryStatus == BatteryStatus.Full)
        {
            changeMaterial(lamp1, On);
        }
        else
        {
            changeMaterial(lamp1, Off);
        }
    }

    void changeMaterial(GameObject o, Material m)
    {
        Material[] mats = o.GetComponent<Renderer>().materials;
        mats[0] = m;
        o.GetComponent<Renderer>().materials = mats;
    }

}
