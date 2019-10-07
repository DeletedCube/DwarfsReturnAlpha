using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int WinHexLineLength = 5; //выигрышное колво гексов в линии
    public TileSpawner Spawner; //доступ к спаунеру

    void Start()
    {
        LetsStart();
    }

    void LetsStart()
    {
        HexTile tile1 = Spawner.AddInstance(new Vector3Int(0, 0, 0));
       // HexTile tile2 = Spawner.AddInstance(new Vector3Int(1, -1, 0));
       // tile1.OnPointerClick(null);
       // tile2.OnPointerClick(null);
    }

    //проверка на выигрыш
    public void LetsWin(Vector3Int _checkHex, Turn.Side _turnSide)
    {
        if (WinCheck(_checkHex, _turnSide))
        {
            Debug.LogWarning("Win!");
        }

    }

    // проверка на линию у гекса по трем осям
    bool WinCheck(Vector3Int _curHex, Turn.Side _turnSide)
    {
        int[] Count = { 1, 1, 1 }; //счетчик по трем осям

        int maxCount = 0;
        for (int i = 0; i < 3; i++) 
        {
            Count[i] += CountTilesInDirection(_curHex, MapStorage.Neighbors[i], _turnSide); //проверка по оси (направление i)
            Count[i] += CountTilesInDirection(_curHex, MapStorage.Neighbors[i + 3], _turnSide); //и в обратную сторону

            maxCount = Mathf.Max(maxCount, Count[i]);//выбирает максимальное значение на оси
        }

        Debug.Log($"{Count[0]}, {Count[1]}, {Count[2]}");
        return maxCount == WinHexLineLength; 
    }

    //считает, сколько гексов той же стороны в направлении
    int CountTilesInDirection(Vector3Int startPos, Vector3Int direction, Turn.Side side)
    {
        int count = 0;
        Vector3Int pos = startPos + direction;
        while (IsSidesEqual(side, Spawner.GetTileAtCoords(pos)))
        {
            count++;
            pos += direction;
        }
        return count;
    }

    //если соседсого тайла нет и он используется(схожен) и его сторона совпадает
    bool IsSidesEqual(Turn.Side _hexSide, HexTile _neighborHex)
    {
        return (_neighborHex != null) && _neighborHex.Used && (_hexSide == _neighborHex.Side);
    }

}
