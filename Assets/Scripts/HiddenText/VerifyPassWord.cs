using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VerifyPassWord : MonoBehaviour
{

    private string password;
    public GameObject inputField;
    public TextMeshProUGUI  texte;
    // Start is called before the first frame update
    void Start()
    {
        password = "layout";
    }

    // Update is called once per frame
    void Update()
    {
   
       
           
    }

    public void VerifPassword()
    {
        if(password.Equals(inputField.GetComponent<TMP_InputField>().text.ToLower()))
        {
            PlayerPrefs.SetInt("hiddentext",1);
            texte.text ="Mot correct";
        }
        else
        {
            texte.text ="Mot incorrect";
        }
    }
}
