using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    AndroidJavaObject camManager;
    bool isOn=false;

    // Start is called before the first frame update
    void Start()
    {
        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

        camManager = new AndroidJavaObject("com.example.flashlightmanager.FlashLightManager");
        camManager.Call("init", activity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            isOn = camManager.Call<bool>("isFlashOn");
            transform.GetComponent<Light>().enabled = isOn;
        }
    }

    /*
    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        bool isFlashOn = camManager.Call<bool>("isFlashOn");
        if (isFlashOn)
        {
            GUILayout.Label("\n\nYeah");
        }
        else
        {
            GUILayout.Label("\n\nTT");
        }
        GUILayout.Label("\n\n\n" + camManager.Call<string>("getCamUpdateId"));
    }
    */
}
