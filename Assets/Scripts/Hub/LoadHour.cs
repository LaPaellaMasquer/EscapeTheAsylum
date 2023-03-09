using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHour : MonoBehaviour, InteractableHubObjectInterface
{
    public void LoadEnigm()
    {
        SceneManager.LoadScene("Hour");
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
