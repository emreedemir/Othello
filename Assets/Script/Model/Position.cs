using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    override public string ToString()
    {
        return "{" + this.X + "," + this.Y + "}";
    }
}
