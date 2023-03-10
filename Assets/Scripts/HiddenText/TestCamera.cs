using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Camera _cam;    
    // Start is called before the first frame update
    void Start()
    {
        var (center,size) = CalculateOrthoSize();
        _cam.transform.position = center;
        _cam.orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private(Vector3 center,float size) CalculateOrthoSize()
    {
        var bounds = new Bounds();
        foreach(var col in FindObjectsOfType<CubeGestion>() ) bounds.Encapsulate(col.GetComponent<MeshRenderer>().bounds);

        bounds.Expand(0.5f);
        var vertical = bounds.size.y;
        var horizontal = bounds.size.x *_cam.pixelHeight / _cam.pixelWidth;

        var size = Mathf.Max(horizontal,vertical) * 0.5f;
        var center = bounds.center + new Vector3(0,0,-10);

        return (center,size+5);
    }
}
