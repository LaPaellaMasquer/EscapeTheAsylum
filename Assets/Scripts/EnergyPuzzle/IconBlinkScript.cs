using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBlinkScript : MonoBehaviour
{
    private float alpha = 0.02f;
    private bool b;

    private Color c;

    // Start is called before the first frame update
    void Start()
    {
        b = true;
        c = gameObject.GetComponent<SpriteRenderer>().color;
        c.a = 0.2f;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerPrefs.GetInt("energy") == 0)
        {
            float current = c.a;
            if (current <= 0.2f) b = true;
            if (current >= 0.8f) b = false;
            if (b) c.a = current + alpha;
            else c.a = current - alpha;
        }
        else c.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = c;

    }
}
