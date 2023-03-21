using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HintShower : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool showed;
    bool available = false;

    // Start is called before the first frame update
    void Start()
    {
        showed = false;

        //if (!PlayerPrefs.HasKey("hint"))
        //{
        //    PlayerPrefs.SetInt("hint", 0);
        //}
        //available = PlayerPrefs.GetInt("hint") != 0;

        if(!available)
        {
            StartCoroutine(ShowButton());
        }

    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(120);
        gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        available = true;
        //PlayerPrefs.SetInt("hint", 1);
    }

    public void ShowHint()
    {
        if (available)
        {
            showed = !showed;
            panel.SetActive(showed);
        }
    }
}
