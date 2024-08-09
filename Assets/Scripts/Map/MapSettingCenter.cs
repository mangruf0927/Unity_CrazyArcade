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

        player.OnBalloonCheck += CheckInstallation;
        player.OnBalloonPlaced += SetStageObject;
        player.OnControllerReceived += GetBalloonController;
    }

    public void SetStageObject(ObjectTypeEnums type, Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.SetStageObjcet(type, x, y);

        Debug.Log("[" + type + "]" + x + ", " + -y);
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

        if(x < 0 || x >= 15 || y > 0 || y <= -13) return true;

        return stageMap.CheckObjectInstallation(x, y);
    }

    public void GetBalloonController(BalloonController controller)
    { 
        balloon = controller; 

        balloon.OnStreamCheck += CheckInstallation;
        balloon.OnBalloonDestroyed += DestroyStageObject;
    }
}
