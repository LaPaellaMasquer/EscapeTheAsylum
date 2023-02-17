using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGestion : MonoBehaviour
{
    [SerializeField]
    public Material mat;
     [SerializeField]
    public int limit;
    // Start is called before the first frame update
    void Start()
    {
        SetMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    Private Function
    */
    private void SetMaterials()
    {
        Material[] mats = GetComponent<Renderer>().materials; 
        mats[0] = mat; 
        GetComponent<Renderer>().materials = mats;
    }
    /*
    Public Function
    */
    public void ChangeOpacity(int valueSensor)
    {
        if(valueSensor < limit)
        {
            //DO SOMETHING
        }
        else
        {
            //COROUTINE WHO DISSAPEAR OBJECT
        }
    }
}
