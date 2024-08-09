using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MapNode
{
    public ObjectTypeEnums ObjectType { get; set; } = ObjectTypeEnums.None; // 기본값 설정
    public bool HasObject { get; set; } = false; // 기본값 설정
}


public class StageMap : MonoBehaviour
{
    private MapNode[,] map = new MapNode[15, 13]; // 13x15 맵

    private void Awake() 
    {   
        InitializeMap();
    }

    public void InitializeMap()
    {
        for(int i = 0; i < 15; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                map[i, j] = new MapNode();
            }
        }
    }

    public void SetStageObjcet(ObjectTypeEnums type, int x, int y)
    {
        map[x, -y].ObjectType = type;
        map[x, -y].HasObject = true;
    }

    public void DestroyStageObject(int x, int y)
    {
        map[x, -y].ObjectType = ObjectTypeEnums.None;
        map[x, -y].HasObject = false;
    }

    public bool CheckObjectInstallation(int x, int y)
    {
        return map[x, -y].HasObject;
    }

    public ObjectTypeEnums CheckObjectType(int x, int y)
    {
        return map[x, -y].ObjectType;
    }
}





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
