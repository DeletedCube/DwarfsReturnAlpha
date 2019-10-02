using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Turn
{
    public static Side Current { get; private set; }
    
    public static void NextTurn()
    {
        if (Current == Side.SideA)
            Current = Side.SideB;
        else
            Current = Side.SideA;
    }
    
    public enum Side
    {
        SideA, SideB
    }
}
