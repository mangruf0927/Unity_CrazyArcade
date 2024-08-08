using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSettingCenter : MonoBehaviour
{
    public StageBlock stageBlock;
    public StageMap stageMap;
    public PlayerController player;
    
    private BalloonController balloon;


    private void Awake() 
    {
        stageBlock.OnGivePosition += SetStageObject;

        player.OnCheckForBalloon += CheckInstallation;
        player.OnSetBalloon += SetStageObject;
        player.OnGetBalloon += GetBalloonController;
    }

    public void SetStageObject(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.SetStageObjcet(x, y);

        Debug.Log(x + ", " + -y);
    }

    public void DestroyStageObject(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.DestroyStageObject(x, y);

        Debug.Log("파괴했어");
    }

    public bool CheckInstallation(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        return stageMap.CheckObjectInstallation(x, y);
    }

    public void GetBalloonController(BalloonController controller)
    { 
        balloon = controller; 
        balloon.OnDestroyBalloon += DestroyStageObject;
    }
}
