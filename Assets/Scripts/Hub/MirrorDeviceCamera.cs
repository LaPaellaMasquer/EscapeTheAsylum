using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorDeviceCamera : MonoBehaviour, InteractableHubObjectInterface
{
    WebCamTexture mCamera = null;
    string debug = "";
    public void LoadEnigm()
    {
        SceneManager.LoadScene("MirrorEnigm");
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = mCamera;
        mCamera.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("\n\n" + debug);
    }*/
}
