using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSphere : MonoBehaviour
{
    private Renderer renderer;

    public float fadeSpeed, deltaFade;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeIn()
    {
        Color color = renderer.material.color;
        float fade = color.a;

        while (fade < 1)
        {
            fade += (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fade);
            renderer.material.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(deltaFade);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = renderer.material.color;
        float fade = color.a;

        while (fade > 0)
        {
            fade -= (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fade);
            renderer.material.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(deltaFade);
        StartCoroutine(FadeIn());
    }
}
