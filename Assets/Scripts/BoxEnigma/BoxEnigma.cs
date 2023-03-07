using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnigma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine(VibrateDuration());

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
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "Box")
                {
                    var objectScript = hit.collider.GetComponent<DragAndRotateBox>();
                }
            }
        }
    }

    
}
