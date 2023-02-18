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

    float valueSensor;
    float ratio;
    Color32 blackColor;
    int alpha;
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.EnableDevice(LightSensor.current);
        alpha = 0;
        ratio = 40f / 25f;
     //   blackColor = new Color32(255, 255, 255, 255);
     
    }

    // Update is called once per frame
    void Update()
    {

        if(LightSensor.current != null)
        {
            if (LightSensor.current.enabled)
            {
                Debug.Log("LightSensor is enabled" + LightSensor.current.lightLevel.ReadValue());
                valueSensor =  LightSensor.current.lightLevel.ReadValue();
                alpha =   (int)(40f - (float)valueSensor * ratio);
                alpha = (alpha>=0)?alpha:0;
                //tmp.color = new Color32(0, 255, 0,(byte)(alpha));
                for(int i =0;i<cubeConteneur.transform.childCount;i++)
                {
                    if(cubeConteneur.transform.GetChild(i).GetComponent<CubeGestion>().limit> alpha)
                    {
                        //CHANGE LA LIMIT
                    }
                
                }
               
            }
                
            else  Debug.Log("LightSensor isn't enabled");

           
        }
        else  Debug.Log("Null" );
    }
    private void OnGUI(){
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("Value " +valueSensor.ToString() +" " + alpha.ToString());
}
}
