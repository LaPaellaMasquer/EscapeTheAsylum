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

    public IEnumerator VibrateDuration()
    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(10.0f);
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
        
    }

    
}
