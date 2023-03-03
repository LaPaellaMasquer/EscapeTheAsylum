using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine;
using TMPro;
   

public class LightSensorRecept : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI tmp;
    [SerializeField]
    GameObject cubeConteneur;

    private float valueSensor;
    /*
    Property
    */
    public float ValueSensor
    {
        get{return valueSensor;}
    }
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);
    }

    // Update is called once per frame
    void Update()
    {

        if(LightSensor.current != null)
        {
            if (LightSensor.current.enabled)
            {
               // Debug.Log("LightSensor is enabled" + LightSensor.current.lightLevel.ReadValue());
                valueSensor =  LightSensor.current.lightLevel.ReadValue();
            }
            else  Debug.Log("LightSensor isn't enabled");
        }
        else  Debug.Log("Null" );
    }
    /*
    Private functions
    */
    private void OnGUI(){
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("Value " +valueSensor.ToString());
    }
}
