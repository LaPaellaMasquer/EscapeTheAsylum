using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using UnityEngine.SceneManagement;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using System;
using TMPro;
using Unity.VisualScripting;

public class MirrorEnigm : MonoBehaviour
{
    // hint
    [SerializeField] GameObject panel;
    [SerializeField] GameObject button;
    bool showed;
    bool available = false;
    DateTime lastTime;
    float deltaTime;
    float timeLeft;

    Vector2 screenSize;
    WebCamTexture mCamera = null;

    Point2f center;
    CircleSegment circle;

    bool isDone;

    public GameObject circleImage;
    public GameObject letterText;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("mirror"))
        {
            PlayerPrefs.SetInt("mirror", 0);
        }
        isDone = PlayerPrefs.GetInt("mirror") != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = new WebCamTexture();

        mCamera = new WebCamTexture();
        GetComponent<RawImage>().defaultMaterial.mainTexture = mCamera;
        mCamera.Play();

        Canvas canvas = GetComponentInParent<Canvas>();
        screenSize = canvas.GetComponent<RectTransform>().sizeDelta;
        GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2((mCamera.width*screenSize.x)/mCamera.height, screenSize.x);

        if (isDone)
        {
            ShowLetter();
        }

        // =============== hint ====================
        showed = false;

        if (!PlayerPrefs.HasKey("hintMirror"))
        {
            PlayerPrefs.SetFloat("hintMirror", 300);
        }
        timeLeft = PlayerPrefs.GetFloat("hintMirror");

        if (timeLeft > 0)
        {
            StartCoroutine(ShowButton());
        }
        else
        {
            PlayerPrefs.SetFloat("hintMirror", 0);
            button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        PlayerPrefs.SetFloat("hintMirror", time);
    }

    IEnumerator ShowButton()
    {
        lastTime = DateTime.Now;
        yield return new WaitForSeconds(timeLeft);
        button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        if (isDone)
        {
            return;
        }

        FindCirles();
    }

    private void FindCirles()
    {
        Mat image = OpenCvSharp.Unity.TextureToMat(mCamera);
        center.X = image.Width / 2;
        center.Y = image.Height / 2;

        Mat grayMat = new Mat();
        Cv2.CvtColor(image, grayMat, ColorConversionCodes.BGR2GRAY);

        Mat blurred = new Mat();
        Cv2.GaussianBlur(grayMat, blurred, new Size(7, 7), 0);
        CircleSegment[] circles = Cv2.HoughCircles(blurred, HoughMethods.Gradient, 1, 20, 50, 30, 150, 200);

        if (circles.Length != 0)
        {
            foreach (CircleSegment c in circles)
            {
                circle = c;
                if (c.Center.DistanceTo(center) <= 10)
                {
                    mCamera.Pause();
                    PlayerPrefs.SetInt("mirror", 1);
                    isDone = true;
                    ShowLetter();
                }
            }
        }
    }

    private void ShowLetter()
    {
        circleImage.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
        circleImage.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetRelative(true).onComplete = () =>
        {
            circleImage.SetActive(false);
            letterText.SetActive(true);
        };
    }

    public void ReturnToHub()
    {
        mCamera.Stop();
        SceneManager.LoadScene("Hub");
    }

    /*private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("\n\n"+ center + " " + circle.Center + " " + circle.Radius + " " + center.DistanceTo(circle.Center));
    }*/
}
