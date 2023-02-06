using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    Rigidbody objectHold = null;
    float objectCameraDistance;

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
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    objectCameraDistance = hit.distance;
                    objectHold = hit.rigidbody;
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

            if((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && objectHold != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Vector3 newForward = ray.direction * objectCameraDistance;
                objectHold.transform.position = ray.origin + newForward;
                objectHold.freezeRotation = true;
            }

            if (touch.phase == TouchPhase.Ended && objectHold != null)
            {
                objectHold.freezeRotation = false;
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
}
