using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

public class VoiceListener : MonoBehaviour
{
    int qSamples = 1024;  // array size
    float refValue  = 0.1f; // RMS value for 0 dB
    float threshold = 0.02f;      // minimum amplitude to extract pitch
    float rmsValue ;   // sound level - RMS
    float dbValue ;    // sound level - dB
    float pitchValue ; // sound pitch - Hz
    private float[] samples; // audio samples
    private float[] spectrum; // audio spectrum
    private float fSample;

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
        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;

        StartListen();
        audioSource.Play();

        for (int i = 0; i < 10; i++)
        {
            listDB.Add(0);
        }
    }


    void Update()
    {


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
         if (dbValue < 0) 
            dbValue = 0; 

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0, currentVal = 0f;
        int maxN = 0;// maxN is the index of max
        for (i = 0; i < qSamples; i++)
        {
            currentVal = spectrum[i];
            if (currentVal > maxV && currentVal > threshold)
            {
                maxV = currentVal;
                maxN = i;
            }
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < qSamples - 1)
        { // interpolate index using neighbours
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (fSample / 2) / qSamples; // convert index to frequency

        //add current db
        listDB.Add(dbValue);
        listDB.RemoveAt(0);
        //audioSource.Stop();
    }
    public void RenderLine()
    {
        int nb = listDB.Count();
        lineRenderer.positionCount = nb;

        Vector3[] listPts = new Vector3[nb];
        int pos = 5;
        for (int i = 0; i < nb; i++)
        {
            listPts[i] = new Vector3(pos, listDB[i]/3, 0);
            pos += 5;
        }
       

        lineRenderer.SetPositions(listPts);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("\n State :" + state);

         AnalyzeSound();
         GUILayout.Label("\n RMS: " + rmsValue.ToString("F2") + " (" + dbValue.ToString("F1") + " dB)\n" + "Pitch: " + pitchValue.ToString("F0") + " Hz");

        RenderLine();
    } 

    public void StartListen()
    {
        state = "StartListen";
        audioSource.clip = Microphone.Start(null, true, 2, maxFreq);
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
