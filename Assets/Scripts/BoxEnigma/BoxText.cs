using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BoxText : MonoBehaviour
{
    private string password;
    public GameObject inputField;
    public TextMeshProUGUI texte;
    public GameObject cap;
    // Start is called before the first frame update
    void Start()
    {
        password = "box";
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void VerifPassword()
    {
        if (password.Equals(inputField.GetComponent<TMP_InputField>().text.ToLower()))
        {
            cap.transform.DOLocalRotate(new Vector3(0f, -90f, 0f), 2f);
        }
    }
}
