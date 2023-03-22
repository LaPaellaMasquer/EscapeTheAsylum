using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;
using TMPro;
using Unity.VisualScripting;

public class HoursEnigma : MonoBehaviour
{
    // hint
    [SerializeField] GameObject panel;
    [SerializeField] GameObject button;
    bool showed;
    bool available = false;
    DateTime lastTime;
    float deltaTime;
    float timeLeft;

    public GameObject clock;
    public GameObject clockObject;
    public GameObject min;
    public GameObject hour;
    public UnityEngine.UI.Text resultText;
    private int targetHour;
    private int targetMinute;
    private bool puzzleSolved = false;
    private bool dragging = false;
    private Transform toDrag;
    private Vector2 prepos = Vector3.zero;
    int currentHour;
    int currentMinute;

    void Start()
    {
        AndroidJavaClass calendarClass = new AndroidJavaClass("java.util.Calendar");
        AndroidJavaObject calendarInstance = calendarClass.CallStatic<AndroidJavaObject>("getInstance");
        targetHour = (calendarInstance.Call<int>("get", 11))%12;
        targetMinute = calendarInstance.Call<int>("get", 12);
        hour.transform.Rotate(Vector3.forward, 0f);
        min.transform.Rotate(Vector3.forward, 0f);
       
        // =============== hint ====================
        showed = false;

        if (!PlayerPrefs.HasKey("hintHour"))
        {
            PlayerPrefs.SetFloat("hintHour", 300);
        }
        timeLeft = PlayerPrefs.GetFloat("hintHour");

        if (timeLeft > 0)
        {
            StartCoroutine(ShowButton());
        }
        else
        {
            PlayerPrefs.SetFloat("hintHour", 0);
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
        PlayerPrefs.SetFloat("hintHour", time);
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

            if (touch.phase == TouchPhase.Began)
            {
                prepos = pos;
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
                Vector2 deltPos = touch.position - prepos;
                toDrag.Rotate(new Vector3(0f, 0f,-(deltPos.x + deltPos.y) * 0.2f));
                prepos = touch.position;
            }
            if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                CalculateTimeFromAngleHour(hour.transform.rotation.eulerAngles.z, out currentHour);
                CalculateTimeFromAngleMinute(min.transform.rotation.eulerAngles.z, out currentMinute);
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

            min.transform.DOLocalRotate(new Vector3(90f, 70f, -200f), 2f);
            hour.transform.DOLocalRotate(new Vector3(90f, -70f, 26f), 2f);
            clockObject.transform.DOMoveY(1.5f, 2f);
        }
    }

    public void CalculateTimeFromAngleHour(float angle, out int hours)
    {
        float radAngle = angle * Mathf.Deg2Rad;

        hours = (Mathf.FloorToInt(radAngle / (2 * Mathf.PI / 12)) + 3) % 12;
    }

    public void CalculateTimeFromAngleMinute(float angle, out int minutes)
    {
        float radAngle = angle * Mathf.Deg2Rad;

        minutes = Mathf.FloorToInt(radAngle / (2 * Mathf.PI / 60) + 15) % 60;
    }


    public void ReturnToHub()
    {
        SceneManager.LoadScene("Hub");
    }

}
