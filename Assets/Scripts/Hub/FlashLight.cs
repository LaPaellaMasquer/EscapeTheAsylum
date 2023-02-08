using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    AndroidJavaObject camManager;
    public bool isOn=false;

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
            transform.GetChild(0).gameObject.SetActive(!isOn);
            transform.GetComponent<Light>().enabled = isOn;
        }
    }
}
