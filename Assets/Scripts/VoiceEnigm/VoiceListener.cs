using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class VoiceListener : MonoBehaviour
{
    bool isComplet;

    int qSamples = 4096;  // array size
    float refValue  = 0.1f; // RMS value for 0 dB
    float rmsValue ;   // sound level - RMS
    float dbValue ;    // sound level - dB
    private float[] samples; // audio samples
    int nbValue = 33;

    private int minFreq;
    private int maxFreq;

    List<float> listDB = new List<float>();
    public LineRenderer lineRenderer;

    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("voice"))
        {
            PlayerPrefs.SetInt("voice", 0);
        }
        isComplet = PlayerPrefs.GetInt("voice") != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Permissions
       /*
        * if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Debug.Log("Microphone permission !");
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
            Debug.Log("No Microphone permission !");
        }
       */
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
        lineRenderer.positionCount = nbValue;

        for (int i = 0; i < nbValue; i++)
        {
            listDB.Add(25);
        }

        if (isComplet) // Enigm completed
        {
            StartCoroutine(ShowLetter());
        }
        else
        {
            StartListen();
            StartAudio();     
            StartCoroutine(ShowLine());
        }
    }

    IEnumerator ShowLetter()
    {
        int startX = 11, startY = 8;
        int nbStep = nbValue-1, currentStep = 0, radius = 8;
        float progress = 0, currentRadian = 0, xCos = 0, ySin = 0;
        float x = 0, y = 0;
        Vector3[] listPts = new Vector3[nbStep];

        for (currentStep = 0; currentStep <= nbStep; currentStep++)
        {
            // calcul 1 step of cercle
            progress = (float)currentStep / nbStep;
            currentRadian = progress * 2 * Mathf.PI;

            xCos = Mathf.Cos(currentRadian);
            ySin = Mathf.Sin(currentRadian);

            x = radius * xCos;
            y = radius * ySin;

            Vector3 currentPos = new Vector3(x+startX, y+startY, 0);
            lineRenderer.SetPosition(currentStep, currentPos);

            yield return new WaitForSeconds(.1f);
        }

        isComplet = true; 
        PlayerPrefs.SetInt("voice", 1);
    }

    IEnumerator ShowLine()
    {
       while(true)
        {
            if(dbValue >= 62)
            {
                StartCoroutine(ShowLetter());
                break;
            }
            else
            {
                AnalyzeSound();
                RenderLine();
            }  
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
        dbValue = (dbValue)*95/160; // normalize
         if (dbValue < 0) 
            dbValue = 0; 
  
        //add current db
        listDB.Add(dbValue);
        listDB.RemoveAt(0);
    }

    public void RenderLine()
    {
        Vector3[] listPts = new Vector3[nbValue];
        float pos = 0;
        for (int i = 0; i < nbValue; i++)
        {
            listPts[i] = new Vector3(pos, (listDB[i])/4, 0);
            pos += 0.65f;
        } 

        lineRenderer.SetPositions(listPts);
    }

    public void StartListen()
    {
        audioSource.clip = Microphone.Start(null, true, 1, maxFreq);
    }

    public void EndListen()
    {
        Microphone.End(null);
    }

    public void StartAudio()
    {
        audioSource.Play();
    }

    public void ReturnToHub()
    {
        EndListen();
        SceneManager.LoadScene("Hub");
    }

}
