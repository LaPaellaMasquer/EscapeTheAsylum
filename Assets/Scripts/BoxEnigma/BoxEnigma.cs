using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class BoxEnigma : MonoBehaviour
{
    Vector2 PrevPos = Vector2.zero;
    Vector2 PosDelta = Vector2.zero;
    bool finished = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VibrateDuration());

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


}
