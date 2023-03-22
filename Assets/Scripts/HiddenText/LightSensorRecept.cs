using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using System;
using Unity.VisualScripting;

public class LightSensorRecept : MonoBehaviour {

    // hint
    [SerializeField] GameObject panel;
    [SerializeField] GameObject button;
    bool showed;
    bool available = false;
    DateTime lastTime;
    float deltaTime;
    float timeLeft;

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
        // =============== hint ====================
        showed = false;

        if (!PlayerPrefs.HasKey("hintText"))
        {
            PlayerPrefs.SetFloat("hintText", 300);
        }
        timeLeft = PlayerPrefs.GetFloat("hintText");

        if (timeLeft > 0)
        {
            StartCoroutine(ShowButton());
        }
        else
        {
            PlayerPrefs.SetFloat("hintText", 0);
            button.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            available = true;
        }

    }

    void OnApplicationFocus(bool hasFocus)
    {
        float deltaTime = 0;
        if (hasFocus)
        {
            lastTime = DateTime.Now;
        }
        else
        {
            SaveTime();
        }
    }

    void SaveTime()
    {
        float deltaTime = DateTime.Now.Subtract(lastTime).Minutes * 60 + DateTime.Now.Subtract(lastTime).Seconds;
        float time = timeLeft - deltaTime;
        if (time < 0)
        {
            time = 0;
        }
        PlayerPrefs.SetFloat("hintText", time);
    }

    IEnumerator ShowButton()
    {
        lastTime = DateTime.Now;
        yield return new WaitForSeconds(timeLeft);
        button.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        available = true;
    }

    public void ShowHint()
    {
        if (available)
        {
            showed = !showed;
            panel.SetActive(showed);
        }
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
    public void ReturnToHub()
    {
        SceneManager.LoadScene("Hub");
    }
    /*
    Private functions
    */
    private void OnGUI(){
    //    GUI.skin.label.fontSize = Screen.width / 40;
      //  GUILayout.Label("Value " +valueSensor.ToString());
    }
    
}
