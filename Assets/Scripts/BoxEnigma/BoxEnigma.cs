using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnigma : MonoBehaviour
{
    Vector2 PrevPos = Vector2.zero;
    Vector2 PosDelta = Vector2.zero;
    

    // Start is called before the first frame update
    void Start()
    {
            //StartCoroutine(VibrateDuration());

    }

    private IEnumerator VibrateDuration()
    {
        WaitForSeconds one = new WaitForSeconds(1.0f);
        WaitForSeconds two = new WaitForSeconds(2.0f);
        int durationShort = 200;
        int durationLong = 700;
        Vibration.Init();
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
        yield return two;

        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(2.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(2.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();
        //yield return new WaitForSeconds(1.0f);
        //Handheld.Vibrate();

    }

    //private IEnumerator VibrateForSeconds(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    Handheld.Vibrate();

    //}

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

            if(touch.phase == TouchPhase.Moved)
            {
                PosDelta = touch.position - PrevPos;
                Vector3 addAngle = new Vector3(PosDelta.y > 0 ? 1f : -1f, PosDelta.x < 0 ? 1f : -1f, 0f);
                transform.eulerAngles += addAngle;
                PrevPos = touch.position;
            }
        }
    }

    
}
