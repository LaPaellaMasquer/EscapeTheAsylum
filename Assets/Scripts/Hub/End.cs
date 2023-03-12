using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class End : MonoBehaviour
{
    public VideoPlayer videoEnd;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("hiddentext") && PlayerPrefs.GetInt("hiddentext") != 0)
        {
            transform.DORotate(new Vector3(0, -145, 0), 2f);
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            videoEnd.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
