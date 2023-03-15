using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBackGround : MonoBehaviour
{
    //private Camera mainCamera;
    public int baseHeigth,baseWidth;
    public int screenHeight,screenWidth;

    public float ratioW,ratioH;
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height ;

        ratioW = (float)baseWidth / (float)screenWidth;
        ratioH = (float)baseHeigth / (float)screenHeight;

        replaceChildren();
    }

    private void replaceChildren()
    {
        int nbChildren = transform.childCount;
        for(int i =0;i<nbChildren;i++)
        {
            transform.GetChild(i).position = new Vector3(transform.GetChild(i).position.x /ratioW,transform.GetChild(i).position.y /ratioH ,transform.GetChild(i).position.z);
        }
    }

    private void OnGUI(){
     //   GUI.skin.label.fontSize = Screen.width / 40;
       // GUI.Label(new Rect(10, 450, 4000, 40),"rW" + ratioW +" : rH" + ratioH );
       //  GUI.Label(new Rect(10, 500, 4000, 40),"sW" + screenWidth +" : sH" + screenHeight );
    }
}
