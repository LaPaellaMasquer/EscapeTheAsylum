using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GyroCamera : MonoBehaviour
{
    MovingObject objectHold = null;
    float objectCameraDistance;
    string debug;

    // Start is called before the first frame update
    void Start()
    {

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GyroModifyCamera();
        if (Input.touchCount != 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    objectCameraDistance = hit.distance;
                    objectHold = hit.transform.GetComponent<MovingObject>();
                    debug = hit.transform.name;
                    if (objectHold == null)
                    {
                        InteractableHubObjectInterface enigm = hit.transform.GetComponent<InteractableHubObjectInterface>();
                        if (enigm != null)
                        {
                            enigm.LoadEnigm();
                        }
                    }
                }
            }

            if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && objectHold != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Vector3 newForward = ray.direction * objectCameraDistance;
                objectHold.MoveObject(ray.origin + newForward);
            }

            if (touch.phase == TouchPhase.Ended && objectHold != null)
            {
                objectHold.Release();
                objectHold = null;
            }
        }
    }

    void GyroModifyCamera()
    {
        transform.Rotate(GyroToUnity(Input.gyro.rotationRateUnbiased)*2f);
    }

    private  Vector3 GyroToUnity(Vector3 v)
    {
        return new Vector3(v.x < 0.1f && v.x > -0.1f ? 0 : -v.x, v.y < 0.1f && v.y > -0.1f ? 0 : -v.y, v.z < 0.1f && v.z > -0.1f ? 0 : v.z) ;
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("\n\n" + debug);
    }
}
