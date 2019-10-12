using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapStorage
{

    static Dictionary<Turn.Side, Material> materials;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadMaterials()
    {
        materials = new Dictionary<Turn.Side, Material>();

        Material matA = Resources.Load<Material>(Turn.Side.SideA.ToString());
        materials.Add(Turn.Side.SideA, matA);

        Material matB = Resources.Load<Material>(Turn.Side.SideB.ToString());
        materials.Add(Turn.Side.SideB, matB);
    }
    public static Material GetCurrentTurnMaterial()
    {
        Material mat;
        materials.TryGetValue(Turn.Current, out mat);
        return mat;
    }

    public static Dictionary<Vector3Int, HexTile> Tiles = new Dictionary<Vector3Int, HexTile>();

    public static Vector3 HexToWorld(Vector3Int cubePosition)
    {
        cubePosition.y = -cubePosition.x - cubePosition.z;
        float horizPosition = 1.5f * cubePosition.x;
        float vertPosition = Mathf.Sqrt(3.0f) * (cubePosition.x * 0.5f + cubePosition.y);

        return new Vector3(horizPosition, 0.0f, vertPosition);
    }

    public static Vector3Int[] Neighbors = new Vector3Int[]
    {
        new Vector3Int( 0, +1, -1),
        new Vector3Int( 1,  0, -1),
        new Vector3Int( 1, -1,  0),
        new Vector3Int( 0, -1, +1),
        new Vector3Int(-1,  0, +1),
        new Vector3Int(-1, +1,  0),
    };

   /* public static Vector3 NeigbourIndexToEuler(int neighbor)
    {


        return 
    }*/


}
