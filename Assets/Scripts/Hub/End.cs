using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class End : MonoBehaviour
{
    public bool isOpen;
    public VideoPlayer videoEnd;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && PlayerPrefs.HasKey("hiddentext") && PlayerPrefs.GetInt("hiddentext") != 0)
        {
            transform.DOLocalRotate(new Vector3(0, -145, 0), 2f);
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            videoEnd.Play();
            isOpen = true;
        }
    }
}
