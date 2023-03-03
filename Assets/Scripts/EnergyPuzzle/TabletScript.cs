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

        if (LastPiece.isLink(3) && LastPiece.getOn())
        {
            solved = true;
            lamp2.setOn(0);
        }
        
        if(solved && !display)
        {
            displaySolution();
        }
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

}
