using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBlock : MonoBehaviour
{
    public delegate void blockHandler(ObjectTypeEnums type, Vector2 pos);
    public event blockHandler OnGivePosition;

    public List<GameObject> ObjectList;

    private void Start() 
    {
        foreach(GameObject obj in ObjectList)
        {
            OnGivePosition?.Invoke(ObjectTypeEnums.Object, obj.transform.position);
        }    
    }
}
