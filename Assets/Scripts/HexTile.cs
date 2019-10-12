using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour, IPointerClickHandler
{
    public TileSpawner Spawner;
    public Renderer HexRenderer;
    public GameObject Selector;

    private Vector3Int CubePosition = new Vector3Int(0, 0, 0); //запись своего положения в сцене
    private Vector3 EulerHexDirection = new Vector3(0.0f, 0.0f, 0.0f);

    public Turn.Side Side { get; private set; } //гекс запоминает свою сторону
    public bool isEmptyHex { get; private set; } //флаг схоженного гекса, гекс запоминает свое состояние

    private bool Selected = false;


    //позиция при установке в сцене и флаг "несхоженного" гекса
    public void PlaceHex(Vector3Int _cubePosition)
    {
        this.transform.position = MapStorage.HexToWorld(_cubePosition);
        this.transform.localRotation = Quaternion.Euler(EulerHexDirection);
        CubePosition = _cubePosition;
        isEmptyHex = true;

    }

    //действия при клике
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!Selected) SelectHex();
        else AddHexes();

        
    }

    private void SelectHex()
    {
        Selected = true;
        Selector.SetActive(Selected);
    }
    private void AddHexes()
    {
        Spawner.AddNeighbors(CubePosition);
        Debug.Log("Try to add");
    }

    private void DeselectHex()
    {
        Selected = false;
        Selector.SetActive(Selected);
    }

}
