using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletScript : MonoBehaviour
{

    public LedScript lamp1;
    public LedScript lamp2;

    public PieceScript FirstPiece;
    public PieceScript LastPiece;

    public PieceScript[] pieces;
    
    public bool solved;

    private bool charging, display;
    private bool isRunning;
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("energy"))
        {
            PlayerPrefs.SetInt("energy", 0);
        }
        solved = PlayerPrefs.GetInt("energy") != 0;
        display = false;
        isRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        FirstPiece.setFirst();

    }

    // Update is called once per frame
    void Update()
    {
        charging = (SystemInfo.batteryStatus == BatteryStatus.Charging);
        
        if (charging)
        {
            lamp1.setOn(0);
            if(!display) FirstPiece.setOn();
        }
        else
        {
            lamp1.setOff(0);
            if (!display) FirstPiece.setOff();
        }

        if (LastPiece.isLink(2) && LastPiece.getOn() )
        {
            if(!isRunning)
                StartCoroutine(WaitCoroutine());
        }
        
        if(solved)
        {
            lamp2.setOn(0);
            displaySolution();
        }
    }

    IEnumerator WaitCoroutine()
    {
        isRunning = true;
        yield return new WaitForSeconds(2);

        solved = true;
        PlayerPrefs.SetInt("energy", 1);
        isRunning = false;
    }

    private void displaySolution()
    {
        foreach(PieceScript piece in pieces)
        {
            if(piece != null)
                piece.display();
        }
        display = true;
    }
    private void OnGUI()
    {
         GUI.skin.label.fontSize = Screen.width / 40;
        GUI.Label(new Rect(10, 70  *50, 4000, 40),"SensorValue" +isRunning);
       // GUILayout.Label("Value " +alpha.ToString() );
    }
}
