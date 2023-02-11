using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    AndroidJavaObject camManager;
    private bool isOn;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("flashlight"))
        {
            PlayerPrefs.SetInt("flashlight", 0);
        }
        isOn = PlayerPrefs.GetInt("flashlight")!=0;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isOn)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            camManager = new AndroidJavaObject("com.example.flashlightmanager.FlashLightManager");
            camManager.Call("init", activity);
        }
        else
        {
            DestroyImmediate(transform.GetChild(0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            isOn = camManager.Call<bool>("isFlashOn");
            transform.GetComponent<Light>().enabled = isOn;

            if (isOn)
            {
                PlayerPrefs.SetInt("flashlight", 1);
                DestroyImmediate(transform.GetChild(0));
            }
        }
    }
   
}
