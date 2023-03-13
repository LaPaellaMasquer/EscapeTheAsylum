using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
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
    }
    public void LoadEnigm()
    {
        SceneManager.LoadScene("HiddenText");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!isUnlock && PlayerPrefs.HasKey("hiddentext") && PlayerPrefs.GetInt("hiddentext") != 0)
        {
            videoPlayer.clip = unlockVideo;
            isUnlock = true;
        }
    }
}
