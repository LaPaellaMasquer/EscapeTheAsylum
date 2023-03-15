using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using TMPro;
public class VerifyPassWord : MonoBehaviour
{

    private string password;
    public GameObject inputField;
    public TextMeshProUGUI  texte;
    public LocalizeStringEvent  loc;

    private string goodword,badword;
    // Start is called before the first frame update
    void Start()
    {
        password = "layout";
        goodword = LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationTable", "GoodWordHiddenText");
        badword = LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationTable", "BadWordHiddenText");
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
            texte.text = goodword;
        }
        else
        {
            texte.text = badword;
        }
    }
}
