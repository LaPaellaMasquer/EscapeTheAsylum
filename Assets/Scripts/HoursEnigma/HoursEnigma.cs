using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

using static System.Net.Mime.MediaTypeNames;

public class HoursEnigma : MonoBehaviour
{
    public GameObject clock;
    public UnityEngine.UI.Text resultText;
    private int targetHour;
    private int targetMinute;
    private bool puzzleSolved = false;
    private bool dragging = false;
    private Transform toDrag;
    private Vector3 prepos = Vector3.zero;
    int currentHour;
    int currentMinute;

    void Start()
    {
        AndroidJavaClass calendarClass = new AndroidJavaClass("java.util.Calendar");
        AndroidJavaObject calendarInstance = calendarClass.CallStatic<AndroidJavaObject>("getInstance");
        targetHour = (calendarInstance.Call<int>("get", 11))%12;
        targetMinute = calendarInstance.Call<int>("get", 12);
        clock.transform.Find("HourHand").Rotate(Vector3.forward, 0f);
        clock.transform.Find("MinuteHand").Rotate(Vector3.forward, 0f);
    }

    void Update()
    {
        if (!puzzleSolved)
        {
            if(Input.touchCount != 1)
            {
                dragging = false;
                return;
            }
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;
            prepos = Input.mousePosition;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Clock")
                    {

                        toDrag = hit.transform;
                        dragging = true;
                    }
                }
            }
            if (dragging && touch.phase == TouchPhase.Moved)
            {
                Vector2 t = touch.position;
                Vector2 raw = touch.rawPosition;
                t.Normalize();
                raw.Normalize();
                Vector3 newRotation = toDrag.rotation.eulerAngles;
                newRotation.z += (t.y>raw.y? -Mathf.Acos(Vector2.Dot(t, raw)):Mathf.Acos(Vector2.Dot(t, raw)))*2;
                toDrag.rotation = Quaternion.Euler(newRotation);
            }
            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                CalculateTimeFromAngleHour(clock.transform.Find("HourHand").rotation.eulerAngles.z, out currentHour);
                CalculateTimeFromAngleMinute(clock.transform.Find("MinuteHand").rotation.eulerAngles.z, out currentMinute);
                if (currentHour < targetHour + 0.5 && currentHour > targetHour - 0.5 && currentMinute < targetMinute + 5 && currentMinute > targetMinute - 5)
                {
                    puzzleSolved = true;
                }
                toDrag = null;
                dragging = false;
            }

        }
        else
        {
            clock.transform.DOMove(new Vector3(0, 0, 0), 4f);
        }
    }

    public void CalculateTimeFromAngleHour(float angle, out int hours)
    {
        float radAngle = angle * Mathf.Deg2Rad;

        hours = (Mathf.FloorToInt(radAngle / (2 * Mathf.PI / 12)) % 12)+3;
    }

    public void CalculateTimeFromAngleMinute(float angle, out int minutes)
    {
        float radAngle = angle * Mathf.Deg2Rad;

        minutes = Mathf.FloorToInt(radAngle / (2 * Mathf.PI / 60))+15;
    }

    private void OnGUI()
    {
        if (puzzleSolved)
        {
            GUI.color = Color.black;
            GUI.skin.label.fontSize = Screen.width / 40;
            GUILayout.Label("\n\nSolved");

            
        }
        else
        {
            GUI.color = Color.black;
            GUI.skin.label.fontSize = Screen.width / 40;
            GUILayout.Label("\n\nUnsolved" + targetHour+"h " + targetMinute + "min " + currentMinute);
        }
    }


}
