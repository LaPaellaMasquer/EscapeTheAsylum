using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGestion : MonoBehaviour
{
    [SerializeField]
    public Material mat;//Material Associate
    [SerializeField]
    public float limitSup;//Light sensor limit
    [SerializeField]
    public float limitInf;//Light sensor limit
    private float alpha;//actual alpha 
    public LightSensorRecept sensor;// actual light sensor
    public int id;
    private bool isRunning;
    private float fadeSpeed;
    private float time;
    private bool isDone;
    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("hiddentext"))
        {
            PlayerPrefs.SetInt("hiddentext", 0);
        }
        isDone = PlayerPrefs.GetInt("hiddentext") != 0;
        SetMaterials();
        sensor = gameObject.transform.parent.GetComponent<LightSensorRecept>();
        fadeSpeed = 1.5f;
        alpha = 0.0f;
        isRunning = false;
        time =0f;
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        if(!isRunning && time> 2f)//starts coroutine when alpha change
        {
            if(sensor.ValueSensor > limitInf && sensor.ValueSensor < limitSup && alpha <= 0f)//illumine object
                StartCoroutine(IncreaseAlpha());
            else if((sensor.ValueSensor > limitSup || sensor.ValueSensor < limitInf ) && alpha >= 1f)//make object darker
                StartCoroutine(DecreaseAlpha());
        }
    }
    /*
    Private Function
    */
    //Increase alpha from 0 to 1
    private IEnumerator IncreaseAlpha()
    {
        isRunning = true;
        var mat = gameObject.GetComponent<MeshRenderer>().material;
        while(alpha <=1f)
        {
            alpha += (fadeSpeed * Time.deltaTime);
            ApplyAlpha(mat,alpha);
            yield return null;
        }
        alpha =1f;
        isRunning = false;
        yield return null;
    }
     //Decrease alpha from 1 to 0
   private IEnumerator DecreaseAlpha()
    {
        isRunning = true;
        var mat = gameObject.GetComponent<MeshRenderer>().material;
        //Debug.Log("is enabled" +isRunning);
        while(alpha >=0f)
        {
            alpha -= (fadeSpeed * Time.deltaTime);
            ApplyAlpha(mat,alpha);
            yield return null;
        }
        ApplyAlpha(mat,0f);
        alpha = 0f;
        isRunning = false;
        yield return null;
    }
    //Set the materials give in entry 
    private void SetMaterials()
    {
        Material[] mats = GetComponent<Renderer>().materials; 
        Color newColor = new Color(mat.color.r,mat.color.g,mat.color.b,0f);
        mat.color = newColor;
        mats[0] = mat; 
        GetComponent<Renderer>().materials = mats;
    }

    // apply the alpha on the material
    private void ApplyAlpha(Material mat, float newAlpha)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r,oldColor.g,oldColor.b,newAlpha);
        mat.color = newColor;   
    }
    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUI.Label(new Rect(10, 70 + id *50, 4000, 40), limitSup +" : Alpha " + alpha);
       // GUILayout.Label("Value " +alpha.ToString() );
    }
}
