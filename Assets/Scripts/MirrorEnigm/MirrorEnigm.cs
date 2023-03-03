using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class MirrorEnigm : MonoBehaviour
{
    Vector2 screenSize;
    WebCamTexture mCamera = null;

    Point2f center;
    CircleSegment circle;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!mCamera.isPlaying)
        {
            return;
        }

        Mat image = OpenCvSharp.Unity.TextureToMat(mCamera);
        center.X = image.Width / 2;
        center.Y = image.Height / 2;

        Mat grayMat = new Mat();
        Cv2.CvtColor(image, grayMat, ColorConversionCodes.BGR2GRAY);

        Mat blurred = new Mat();
        Cv2.GaussianBlur(grayMat, blurred, new Size(7, 7), 0);
        CircleSegment[] circles = Cv2.HoughCircles(blurred, HoughMethods.Gradient, 1, 20, 50, 30, 150, 200);

        if (circles.Length!=0)
        {
            foreach (CircleSegment c in circles)
            {
                circle = c;
                if(c.Center.DistanceTo(center) <= 10)
                {
                    mCamera.Pause();
                    Cv2.Circle(image, (int)c.Center.X, (int)c.Center.Y, (int)c.Radius, new Scalar(0, 0, 255), 2);
                    GetComponent<RawImage>().texture = OpenCvSharp.Unity.MatToTexture(image);
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("\n\n"+ center + " " + circle.Center + " " + circle.Radius + " " + center.DistanceTo(circle.Center));
    }
}
