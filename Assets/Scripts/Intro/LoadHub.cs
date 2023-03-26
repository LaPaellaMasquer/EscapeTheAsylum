
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using System.Collections.Generic;
using System;
public class LoadHub : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(AskPerm());
    }

   
    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator AskPerm()
    {
        #if UNITY_ANDROID
            List<bool> permissions = new List<bool>() { false, false  };
            List<bool> permissionsAsked = new List<bool>() { false, false };
            List<Action> actions = new List<Action>()
            {
                new Action(() => {
                    permissions[0] = Permission.HasUserAuthorizedPermission(Permission.Microphone);
                    if (!permissions[0] && !permissionsAsked[0])
                    {
                        Permission.RequestUserPermission(Permission.Microphone);
                        permissionsAsked[0] = true;
                        return;
                    }
                }),
                new Action(() => {
                    permissions[1] = Permission.HasUserAuthorizedPermission(Permission.Camera);
                    if (!permissions[1] && !permissionsAsked[1])
                    {
                        Permission.RequestUserPermission(Permission.Camera);
                        permissionsAsked[1] = true;
                        return;
                    }
                })
            };
            for(int i = 0; i < permissionsAsked.Count; )
            {
                actions[i].Invoke();
                if(permissions[i])
                {
                    ++i;
                }
                yield return new WaitForEndOfFrame();
            }
        #endif

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Hub");
    }


    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
