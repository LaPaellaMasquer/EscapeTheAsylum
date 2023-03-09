using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        isEnd = PlayerPrefs.GetInt("hiddentext") != 0;
        transform.Rotate(new Vector3(0, 90, 0));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
