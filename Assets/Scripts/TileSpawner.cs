using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public TileSpawner Spawner; //ссылается на себя, чтобы потом передавать новым гексам
    public HexTile Prefab;

    //создание нового гекса в сцене
    public HexTile AddInstance(Vector3Int _cubePosition)
    {
        //проверка на соответсвие кубическим координатам
        if (_cubePosition.x + _cubePosition.y + _cubePosition.z != 0)
        {
            Debug.LogError($"Vector3Int {_cubePosition} is not suitable for cubic coordinates");
            return null;
        }
        else
        {
            if (CheckCoords(_cubePosition)) //если у тайла координаты заняты - прерывает
                return null;

            HexTile newHexTile = Instantiate(Prefab); //вставляет префаб
            newHexTile.transform.SetParent(transform); //делает его своим подобъектом в сцене
            newHexTile.gameObject.name = $"Hex {_cubePosition}"; //добавляет к имени кубические координаты

            newHexTile.PlaceHex(_cubePosition); //ставит тайл на позицию
            newHexTile.Spawner = Spawner; //записывает ему себя
            SetHexToCoords(_cubePosition, newHexTile); //записывает координаты в словарь

            return newHexTile;
        }
    }

    //пробегается по соседним тайлам и если свободны, добавляет пустые гексы
    public void AddNeighbors(Vector3Int _hexPos)
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3Int pos = _hexPos + MapStorage.Neighbors[i];
            AddInstance(pos);
        }
    }

    //связвает гекс с координатами
    public void SetHexToCoords(Vector3Int _pos, HexTile tile)
    {
        MapStorage.Tiles[_pos] = tile;
    }

    //проверка на занятость тайла
    public bool CheckCoords(Vector3Int _pos)
    {
        return MapStorage.Tiles.ContainsKey(_pos);
    }

    //получить доступ к тайлу по координатам
    public HexTile GetTileAtCoords(Vector3Int _pos)
    {
        HexTile tile = null;
        MapStorage.Tiles.TryGetValue(_pos, out tile);
        return tile;
    }
}