using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSettingCenter : MonoBehaviour
{
    public StageBlock stageBlock;
    public StageMap stageMap;

    private void Awake() 
    {
        stageBlock.OnGivePosition += GivePosition;
    }

    public void GivePosition(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.SetStageMap(x, y);

        Debug.Log(x + ", " + -y);
    }
}
