using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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

        while (!finished)
        {
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            Vibration.VibrateAndroid(durationShort);
            yield return two;
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            Vibration.VibrateAndroid(durationLong);
            yield return two;
            Vibration.VibrateAndroid(durationLong);
            yield return one;
            Vibration.VibrateAndroid(durationShort);
            yield return one;
            Vibration.VibrateAndroid(durationShort);
            yield return one;
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
                Vector3 addAngle = new Vector3(PosDelta.y > 0 ? 1f : -1f, PosDelta.x < 0 ? 1f : -1f, 0f);
                transform.eulerAngles += addAngle;
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
