using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public struct MapNode
{
    private ObjectTypeEnums objectType;
    private bool hasObject;

    // 생성자
    public MapNode(ObjectTypeEnums type, bool hasObject)
    {
        this.objectType = type;
        this.hasObject = hasObject;
    }

    public ObjectTypeEnums ObjectType
    {
        get { return objectType; }
        set { objectType = value; }
    }

    public bool HasObject
    {
        get { return hasObject; }
        set { hasObject = value; } // setter를 통해 상태 변경 가능
    }
}

public class StageMap : MonoBehaviour
{
    private MapNode[,] map = new MapNode[13, 15]; // 13x15 맵

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
                map[i, j].ObjectType = ObjectTypeEnums.None;
                map[i, j].HasObject = false;
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
