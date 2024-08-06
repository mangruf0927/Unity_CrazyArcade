using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/* public struct MapNode
{
    private Vector2 position;
    private bool hasObject;

    // 생성자
    public MapNode(Vector2 position, bool hasObject)
    {
        this.position = position;
        this.hasObject = hasObject;
    }

    // 위치 접근자
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    // 오브젝트 존재 여부 접근자
    public bool HasObject
    {
        get { return hasObject; }
        set { hasObject = value; } // setter를 통해 상태 변경 가능
    }
} */


public class StageMap : MonoBehaviour
{
    private bool[,] map = new bool[13, 15]; // 13x15 맵

    private void Awake() 
    {   
        InitializeMap();
    }

    public void InitializeMap()
    {
        for(int i = 0; i < 13; i++)
        {
            for(int j = 0; j < 15; j++)
            {
                map[i, j] = false;
            }
        }
    }

    public void SetStageMap(int x, int y)
    {
        map[x, -y] = true;
    }
}
