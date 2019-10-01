using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Turn
{
    public static Side Current { get; private set; }

    static Dictionary<Side, Material> materials;

    //подтягивание материалов из папки Resources по имени
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadMaterials()
    {
        materials = new Dictionary<Side, Material>();

        Material matA = Resources.Load<Material>(Side.SideA.ToString());
        materials.Add(Side.SideA, matA);

        Material matB = Resources.Load<Material>(Side.SideB.ToString());
        materials.Add(Side.SideB, matB);
    }

    public static void NextTurn()
    {
        if (Current == Side.SideA)
            Current = Side.SideB;
        else
            Current = Side.SideA;
    }

    public static Material GetCurrentTurnMaterial()
    {
        Material mat;
        materials.TryGetValue(Current, out mat);
        return mat;
    }

    public enum Side
    {
        SideA, SideB
    }
}
