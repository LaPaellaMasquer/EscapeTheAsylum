using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadHiddenText : MonoBehaviour, InteractableHubObjectInterface
{
    private bool isUnlock;

    public VideoPlayer videoPlayer;
    public VideoClip unlockVideo;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("hiddentext"))
        {
            PlayerPrefs.SetInt("hiddentext", 0);
        }
        isUnlock = PlayerPrefs.GetInt("hiddentext") != 0;
    }
    public void LoadEnigm()
    {
        SceneManager.LoadScene("HiddenText");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isUnlock)
        {
            videoPlayer.clip = unlockVideo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
