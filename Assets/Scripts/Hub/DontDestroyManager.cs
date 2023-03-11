using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyManager : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        transform.Rotate(GyroToUnity(Input.gyro.rotationRateUnbiased)*2f);
    }

    private Vector3 GyroToUnity(Vector3 v)
    {
        return new Vector3(v.x < 0.1f && v.x > -0.1f ? 0 : -v.x, v.y < 0.1f && v.y > -0.1f ? 0 : -v.y, v.z < 0.1f && v.z > -0.1f ? 0 : v.z);
    }
}
