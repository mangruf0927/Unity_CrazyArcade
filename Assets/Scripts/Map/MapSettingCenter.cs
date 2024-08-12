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
    private Dictionary<Vector2, BalloonController> balloonDictionary = new Dictionary<Vector2, BalloonController>();


    private void Awake() 
    {
        stageBlock.OnBlockInstall += SetStageObject;

        foreach(MovableBox box in stageBlock.MovableBoxeList)
        {
            box.OnMoveCheck += CheckInstallation;
            box.OnRegisterNewPos += SetStageObject;
            box.OnRemoveOriginPos += DestroyStageObject;
        }

        player.OnBalloonCheck += CheckInstallation;
        player.OnBalloonPlaced += SetStageObject;
        player.OnControllerReceived += GetBalloonController;
    }

    public void SetStageObject(ObjectTypeEnums type, Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.SetStageObjcet(type, x, y);

        // Debug.Log("[" + type + "]" + x + ", " + -y);
    }

    public void DestroyStageObject(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.DestroyStageObject(x, y);
    }

    public bool CheckInstallation(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        if(x < 0 || x >= 15 || y > 0 || y <= -13) return true;

        return stageMap.CheckObjectInstallation(x, y);
    }

    public ObjectTypeEnums CheckObjectType(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        if(x < 0 || x >= 15 || y > 0 || y <= -13) return ObjectTypeEnums.Object;

        return stageMap.CheckObjectType(x, y);
    }

    public void RemoveBox(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageBlock.RemoveBox(pos);
        stageMap.DestroyStageObject(x, y);
    }

    public void PopBalloon(Vector2 pos)
    {
        if (!balloonDictionary.TryGetValue(pos, out BalloonController balloon)) return;

        balloonDictionary.Remove(pos);
        balloon.StartChangeState(0.1f, BalloonStateEnums.POP);
    }

    public void GetBalloonController(Vector2 pos, BalloonController controller)
    { 
        balloon = controller; 
        balloonDictionary[pos] = controller;

        balloon.OnStreamCheck += CheckObjectType;
        balloon.OnRemoveBox += RemoveBox;
        balloon.OnBalloonDestroyed += DestroyStageObject;
    }
}
