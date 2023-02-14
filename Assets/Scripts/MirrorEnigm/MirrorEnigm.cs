using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class MirrorEnigm : MonoBehaviour
{
    bool isCircle;
    int nbpoint;
    WebCamTexture mCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = new WebCamTexture();
        GetComponent<Image>().defaultMaterial.mainTexture = mCamera;
        mCamera.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Mat image = OpenCvSharp.Unity.TextureToMat(mCamera);

        Mat grayMat = new Mat();
        Cv2.CvtColor(image, grayMat, ColorConversionCodes.BGR2GRAY);

        Mat thresh = new Mat();
        Cv2.Threshold(grayMat, thresh, 127, 255, ThresholdTypes.BinaryInv);

        Point[][] contours;
        HierarchyIndex[] hierarchy;
        Cv2.FindContours(thresh, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxNone, null);


        foreach (Point[] contour in contours)
        {
            double length = Cv2.ArcLength(contour, true);
            Point[] approx = Cv2.ApproxPolyDP(contour, length * 0.01, true);
            isCircle = approx.Length >= 15;
            nbpoint = approx.Length;
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        if (isCircle)
        {
            GUILayout.Label("\n\nYeah");
        }

        GUILayout.Label("\n\n\n" + nbpoint);
    }
        
}
