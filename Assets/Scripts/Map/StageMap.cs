using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MapNode
{
    public ObjectTypeEnums ObjectType { get; set; } = ObjectTypeEnums.None; // 기본값 설정
    public bool HasObject { get; set; } = false; // 기본값 설정
    public BalloonController balloon;
}


public class StageMap : MonoBehaviour
{
    private MapNode[,] map = new MapNode[15, 13]; // 13x15 맵

    private void Awake() 
    {   
        InitializeMap();
    }

    public MapNode Return(Vector2 pos)
    {
        return map[(int)pos.x, -(int)pos.y];
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

    public void SetStageEnemy(ObjectTypeEnums type, int x, int y)
    {
        map[x, -y].ObjectType = type;
        map[x, -y].HasObject = false;
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

    public void ChangeObjectType(ObjectTypeEnums type, int x, int y)
    {
        map[x, -y].ObjectType = type;
    }
}

