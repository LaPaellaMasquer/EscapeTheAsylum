using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript: MonoBehaviour
{
    public GameObject obj;

    public LedScript led;

    private bool solved, first;

    public bool solution;

    // outputs depends on the shape of the piece,index indicates the orientation: 0 = up, 1 = right, 2 = down, 3 = left;
    public bool[] output = new bool[4];

    public PieceScript[] neighbors = new PieceScript[4];

    private bool isOn;
    private bool[] src = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        int i = Random.Range(1, 4);
        for (int j = 0; j < i; j++)rotate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!first && !solved) check();

        if (isOn)
        {
            led.setOn(1);
        }
        else
        {
            led.setOff(1);
        }
    }

    private void check()
    {
        isOn = false;
        for(int i=0; i < 4; i++)
        {
            if (neighbors[i] != null && output[i])
            {
                if (neighbors[i].isLink(i))
                {
                    if (neighbors[i].isOn)
                    {
                        src[i] = true;
                        isOn = true;
                    }
                } else
                {
                    src[i] = false;
                }
            }
        }
    }

    public bool isLink(int neighbor)
    {
        int index = neighborPos(neighbor);

        if (output[index] && !src[index])
        {
            return true;
        }
        return false;
    }

    private int neighborPos(int neighbor)
    {
        switch (neighbor)
        {
            case 0: return 2;
            case 1: return 3;
            case 2: return 0;
            case 3: return 1;
            default: return -1;
        }
    }

    public void setOn()
    {
        isOn = true;
    }

    public void setOff()
    {
        isOn = false;
    }

    public bool getOn()
    {
        return isOn;
    }

    public void touched()
    {
        rotate();
    }

    public void rotate()
    {
        outputShift();
        objRotation();
    }

    private void outputShift()
    {
        bool temp;
        temp = output[3];
        output[3] = output[2];
        output[2] = output[1];
        output[1] = output[0];
        output[0] = temp;
    }

    private void objRotation()
    {
        obj.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
    }

    public void setFirst()
    {
        first = true;
    }

    public void display()
    {
        solved = true;

        if (solution)
        {
            setOn();
        }
        else{
            setOff();
        }
    }
}
