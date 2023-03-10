using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHub : MonoBehaviour
{
    private bool isTextActive;

    public GameObject textStart;

    // Start is called before the first frame update
    void Start()
    {
        isTextActive = true;
        textStart.SetActive(isTextActive);
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("Hub");
            }
        }
    }

    private IEnumerator BlinkText()
    {
        yield return new WaitForSeconds(1);
        isTextActive = !isTextActive;
        textStart.SetActive(isTextActive);
        StartCoroutine(BlinkText());
    }
}
