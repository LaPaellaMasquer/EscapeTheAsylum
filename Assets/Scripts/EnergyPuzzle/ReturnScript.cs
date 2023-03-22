using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ReturnScript : MonoBehaviour
{
    // hint
    [SerializeField] GameObject panel;
    [SerializeField] GameObject button;
    bool showed;
    bool available = false;
    DateTime lastTime;
    float deltaTime;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {

        // =============== hint ====================
        showed = false;

        if (!PlayerPrefs.HasKey("hintPhone"))
        {
            PlayerPrefs.SetFloat("hintPhone", 300);
        }
        timeLeft = PlayerPrefs.GetFloat("hintPhone");

        if (timeLeft > 0)
        {
            StartCoroutine(ShowButton());
        }
        else
        {
            PlayerPrefs.SetFloat("hintPhone", 0);
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
        PlayerPrefs.SetFloat("hintPhone", time);
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void touched()
    {
        SceneManager.LoadScene("Hub");
    }
}
