using UnityEngine;
using System.Collections;

public struct Tile
{
    private bool red_value;
    public bool red
    {
        get { return red_value; }
        set { red_value = value; }
    }

    private bool green_value;
    public bool green
    {
        get { return green_value; }
        set { green_value = value; }
    }

    private bool blue_value;
    public bool blue
    {
        get { return blue_value; }
        set { blue_value = value; }
    }
};

