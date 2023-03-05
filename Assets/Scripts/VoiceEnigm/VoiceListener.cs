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

    //float[] spectrum = new float[8192];
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
            /*
            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
            }
            */       
        }

        samples = new float[qSamples];
        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;

        StartListen();
        audioSource.Play();
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
        // (dbValue < -160) dbValue = -160; // clamp it to -160dB min

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
    }
    public void RenderLine()
    {
        lineRenderer.positionCount = spectrum.Length;

        Vector3[] listPts = new Vector3[qSamples];
        int pos = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            listPts[i] = new Vector3(pos / 10, spectrum[i] / 10, 0);
            pos += 50;
        }
        lineRenderer.SetPositions(listPts);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("\n State :" + state);

        /*
        for (int i=0; i<10; i++)
        {
            GUILayout.Label("\n f : " + spectrum[i]);
        }*/
         AnalyzeSound();
         GUILayout.Label("\n RMS: " + rmsValue.ToString("F2") + " (" + dbValue.ToString("F1") + " dB)\n" + "Pitch: " + pitchValue.ToString("F0") + " Hz");

        RenderLine();
    } 

    public void StartListen()
    {
        state = "StartListen";
        audioSource.clip = Microphone.Start(null, true, 5, maxFreq);
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

        //AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);



    }
}
