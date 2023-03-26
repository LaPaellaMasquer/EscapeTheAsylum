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

    private bool charging, lastCharging, display;

    public void Awake()
    {
        if (!PlayerPrefs.HasKey("energy"))
        {
            PlayerPrefs.SetInt("energy", 0);
        }
        solved = PlayerPrefs.GetInt("energy") != 0;
        display = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        FirstPiece.setFirst();
        lastCharging = false;
        
    }

    public void UpdatePieces()
    {
        if(!solved)
        {
            // reset all pieces 
            foreach (PieceScript p in pieces)
            {
                p.setOn(false);
            }

            if (charging)
            {

                    SetPieces(FirstPiece);

                    // if solved
                    if (LastPiece.getOn() && LastPiece.output[0])
                    {
                        solved = true;
                        StartCoroutine(WaitCoroutine());
                    }
           
        }
 
        }

    }

    // Set On the piece and his childs (recursif)
    void SetPieces(PieceScript piece)
    {
        piece.setOn(true);
       
        for (int i = 0; i < 4; i++)
        {
            if (piece.neighbors[i])
            {
                if (piece.isLink(i) && !piece.neighbors[i].getOn())
                {
                    SetPieces(piece.neighbors[i]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        charging = (SystemInfo.batteryStatus == BatteryStatus.Charging);
        
        if (charging != lastCharging)
        { 
            UpdatePieces();

            lamp1.setOn(charging, 0);

            if (solved)
            {
                if (charging)
                    displaySolution();
                else
                {
                    // reset all pieces 
                    foreach (PieceScript p in pieces)
                    {
                        p.setOn(false);
                    }
                }
                lamp2.setOn(charging, 0);
            } 
            else
                FirstPiece.setOn(charging);

                 
        }
       
        lastCharging = charging;
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2);

        displaySolution();
        lamp2.setOn(charging, 0);
        PlayerPrefs.SetInt("energy", 1);
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
