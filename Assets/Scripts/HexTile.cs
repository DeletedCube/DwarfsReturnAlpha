using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour, IPointerClickHandler
{
    public TileSpawner Spawner;    
    public Renderer HexRenderer;

    private Vector3Int CubePosition = new Vector3Int(0, 0, 0); //запись своего положения в сцене
    private Vector3 EulerHexDirection = new Vector3(0.0f, 0.0f, 0.0f);

    public Turn.Side Side { get; private set; } //гекс запоминает свою сторону
    public bool Used { get; private set; } //флаг схоженного гекса, гекс запоминает свое состояние

    //позиция при установке в сцене и флаг "несхоженного" гекса
    public void PlaceHex(Vector3Int _cubePosition)
    {        
        this.transform.position = MapStorage.HexToWorld(_cubePosition.x, _cubePosition.y, _cubePosition.z);
        this.transform.localRotation = Quaternion.Euler(EulerHexDirection);
        CubePosition = _cubePosition;
        Used = false;

    }

    //действия при клике
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Used)
        {
            Used = true; //гекс становится сходившим
            HexRenderer.material = MapStorage.GetCurrentTurnMaterial(); //устанавливает материал, относительно текущего хода
            Side = Turn.Current; //гекс запоминает свою сторону
            


            Turn.NextTurn(); //следующий ход


            Spawner.AddNeighbors(CubePosition); //вызывает установку пустых гексов вокруг себя
        }
    }
}
