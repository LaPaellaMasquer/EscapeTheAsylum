using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVoiceEnigm : MonoBehaviour, InteractableHubObjectInterface
{
    public void LoadEnigm()
    {
        SceneManager.LoadScene("VoiceEnigm");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
