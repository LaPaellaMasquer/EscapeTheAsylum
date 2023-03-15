using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    AndroidJavaObject camManager;
    private bool isOn;
    private bool isHint;

    public GameObject hintButton;
    public GameObject hint1;

    public GameObject sphere;
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;

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
            hintButton.SetActive(false);
            hint1.SetActive(false);
            StartCoroutine(StartHint());

            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            camManager = new AndroidJavaObject("com.example.flashlightmanager.FlashLightManager");
            camManager.Call("init", activity);

        }
        else
        {
            TurnflashLightOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            isOn = camManager.Call<bool>("isFlashOn");

            if (isOn)
            {
                TurnflashLightOn();
                PlayerPrefs.SetInt("flashlight", 1);
            }
        }
    }

    void TurnflashLightOn()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetComponent<Light>().enabled = true;
        RenderSettings.ambientIntensity = 0.15f;
    }

    IEnumerator StartHint()
    {
        yield return new WaitForSeconds(300);
        hintButton.SetActive(true);
    }

    public void ShowHint()
    {
        if (!isHint)
        {
            ShowHintGoogle();
            isHint = true;
            return;
        }

        if (isHint)
        {
            hint1.SetActive(true);
            hintButton.SetActive(false);
        }
    }

    public void ShowHintGoogle()
    {
        sphere.transform.DOLocalMove(new Vector3(-0.0595f, 0.0228f, -0.0205f), 1);
        sphere1.transform.DOLocalMove(new Vector3(-0.0509f, 0.0372f, -0.0129F), 1);
        sphere2.transform.DOLocalMove(new Vector3(-0.0247f, 0.0329f, -0.0210f), 1);
        sphere3.transform.DOLocalMove(new Vector3(-0.1333f, -0.0320f, 0.0313f), 1);
    }
}
