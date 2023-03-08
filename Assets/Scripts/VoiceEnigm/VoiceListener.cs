using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

public class VoiceListener : MonoBehaviour
{
    int qSamples = 4096;  // array size
    float refValue  = 0.1f; // RMS value for 0 dB
    float rmsValue ;   // sound level - RMS
    float dbValue ;    // sound level - dB
    private float[] samples; // audio samples
    int nbValue = 33;

    private string state;
    private int minFreq;
    private int maxFreq;

    List<float> listDB = new List<float>();
    public LineRenderer lineRenderer;
    [SerializeField] AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        state = "waiting...";

        // Permissions
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Debug.Log("Microphone permission !");
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
            Debug.Log("No Microphone permission !");
        }

        // Check Microphone
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("Microphone not connected!");
        }
        else
        {
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            if (minFreq == 0 && maxFreq == 0) // all freq authorized
            {
                maxFreq = 44100;
            }      
        }

        samples = new float[qSamples];

        StartListen();
        audioSource.Play();

        for (int i = 0; i < nbValue; i++)
        {
            listDB.Add(25);
        }

       // Color c1 = Color.white;
       // Color c2 = new Color(1, 1, 1, 0);
       // lineRenderer.SetColors(c1, c2);

        StartCoroutine(ShowLine());


        }


    void Update()
    {


    }

    IEnumerator ShowLine()
    {
       while(true)
        {
            AnalyzeSound();
            RenderLine();

            yield return new WaitForSeconds(.01f);
        }
    }

    void AnalyzeSound()
    {
        
        audioSource.GetOutputData(samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        dbValue += 160;
        dbValue = (dbValue)*90/160; // normalize
         if (dbValue < 0) 
            dbValue = 0; 
  
        //add current db
        listDB.Add(dbValue);
        listDB.RemoveAt(0);
    }
   
    public void RenderLine()
    {
        lineRenderer.positionCount = nbValue;

        Vector3[] listPts = new Vector3[nbValue];
        float pos = -14;
        for (int i = 0; i < nbValue; i++)
        {
            listPts[i] = new Vector3(pos, (listDB[i])/4+18, 0);
            pos += 0.5f;
        } 

        lineRenderer.SetPositions(listPts);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("\n State :" + state);
        GUILayout.Label("\n RMS: " + rmsValue.ToString("F2") + " (" + dbValue.ToString("F1") + " dB)\n");
    } 

    public void StartListen()
    {
        state = "StartListen";
        audioSource.clip = Microphone.Start(null, true, 1, maxFreq);
    }

    public void EndListen()
    {
        state = "EndListen";
        Microphone.End(null);
    }

    public void StartAudio()
    {
        state = "StartAudio";
        audioSource.Play();


    }
}
