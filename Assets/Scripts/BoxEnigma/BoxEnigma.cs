using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BoxEnigma : MonoBehaviour
{
    // hint
    [SerializeField] GameObject panel;
    [SerializeField] GameObject button;
    bool showed;
    bool available = false;
    DateTime lastTime;
    float deltaTime;
    float timeLeft;

    Vector2 PrevPos = Vector2.zero;
    Vector2 PosDelta = Vector2.zero;
    bool finished = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VibrateDuration());

        // =============== hint ====================
        showed = false;

        if (!PlayerPrefs.HasKey("hintBox"))
        {
            PlayerPrefs.SetFloat("hintBox", 300);
        }
        timeLeft = PlayerPrefs.GetFloat("hintBox");

        if (timeLeft > 0)
        {
            StartCoroutine(ShowButton());
        }
        else
        {
            PlayerPrefs.SetFloat("hintBox", 0);
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
        PlayerPrefs.SetFloat("hintBox", time);
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

    private IEnumerator VibrateDuration()
    {
        WaitForSeconds one = new WaitForSeconds(1.5f);
        WaitForSeconds two = new WaitForSeconds(2.5f);
        int durationShort = 200;
        int durationLong = 700;
        Vibration.Init();

        float tweenVribrationstrength = 0.1f;
        int tweenVibrato = 3;

        while (!finished)
        {

            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);

            Vibration.VibrateAndroid(durationLong);
            yield return one;

            transform.DOShakePosition(durationShort / 100, tweenVribrationstrength, tweenVibrato);

            Vibration.VibrateAndroid(durationShort);
            yield return one;
            transform.DOShakePosition(durationShort / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            transform.DOShakePosition(durationShort / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationShort);
            yield return two;
            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationLong);
            yield return two;
            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            transform.DOShakePosition(durationShort / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            transform.DOShakePosition(durationShort / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            transform.DOShakePosition(durationLong / 100, tweenVribrationstrength, tweenVibrato);
            Vibration.VibrateAndroid(durationLong);
            yield return new WaitForSeconds(5.0f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                PrevPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                PosDelta = touch.position - PrevPos;
                transform.Rotate(new Vector3(0f, -PosDelta.y * 0.2f, -PosDelta.x * 0.2f));
                PrevPos = touch.position;
            }
        }
    }

    public void ReturnToHub()
    {
        finished= true;
        SceneManager.LoadScene("Hub");
    }
    /*
    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("\n\n" + SystemInfo.operatingSystem + "\n" + Vibration.AndroidVersion);
    }
    */
}
