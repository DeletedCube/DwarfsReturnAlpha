using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour, IPointerClickHandler
{
    public Vector3Int CubePosition = new Vector3Int(0, 0, 0); //запись своего положения в сцене
    public TileSpawner Spawner;
    public GameManager Manager;
    public Renderer HexRenderer;

    public Turn.Side Side { get; private set; } //гекс запоминает свою сторону
    public bool Used { get; private set; } //флаг схоженного гекса, гекс запоминает свое состояние

    //позиция при установке в сцене и флаг "несхоженного" гекса
    public void PlaceHex(Vector3Int _cubePosition)
    {
        this.transform.position = HexToWorld(_cubePosition.x, _cubePosition.y, _cubePosition.z);
        CubePosition = _cubePosition;
        Used = false;
    }

    //преобразование кубических координат в мировые
    public static Vector3 HexToWorld(int x, int y, int z)
    {
        y = -x - z;
        float horizPosition = 1.5f * x;
        float vertPosition = Mathf.Sqrt(3.0f) * (x * 0.5f + y);

        return new Vector3(horizPosition, 0.0f, vertPosition);
    }

    //действия при клике
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Used)
        {
            Used = true; //гекс становится сходившим
            HexRenderer.material = Turn.GetCurrentTurnMaterial(); //устанавливает материал, относительно текущего хода
            Side = Turn.Current; //гекс запоминает свою сторону
            Manager.LetsWin(CubePosition, Side); //проверка на победу


            Turn.NextTurn(); //следующий ход


            Spawner.AddNeighbors(CubePosition); //вызывает установку пустых гексов вокруг себя
        }
    }
}
