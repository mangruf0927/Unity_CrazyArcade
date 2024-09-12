using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSettingCenter : MonoBehaviour
{
    [SerializeField]    private StageBlock stageBlock;
    [SerializeField]    private StageMap stageMap;
    [SerializeField]    private StageEnemy stageEnemy;
    [SerializeField]    private PlayerController playerController;
    [SerializeField]    private BossController bossController;
    
    private List<BalloonController> balloonPopList = new List<BalloonController>();
    private int count;

    private void Start() 
    {
        // 오브젝트 이벤트 등록
        stageBlock.OnBlockInstall += SetStageObject;

        // 박스 이벤트 등록
        foreach(MovableBox box in stageBlock.MovableBoxList)
        {
            box.OnGetDirection += GetPlayerDirection;
            box.OnMoveCheck += CheckObjectType;
            box.OnRegisterNewPos += SetStageObject;
            box.OnRemoveOriginPos += DestroyStageObject;
        }

        // enemy 이벤트 등록
        foreach(EnemyController enemy in stageEnemy.enemyList)
        {
            enemy.OnUpdatePosition += SetStageEnemy;
            enemy.OnRemovePosition += DestroyStageObject;
            enemy.OnRemoveEnemy += RemoveEnemy;
        }

        // 플레이어 이벤트 등록
        playerController.OnCheckBalloon += CheckInstallation;
        playerController.OnSetBalloon += SetStageObject;
        playerController.OnControllerReceived += GetBalloonController;

        
        if (bossController != null) 
        {
            bossController.OnSetBalloon += SetStageObject;
            bossController.OnControllerReceived += GetBalloonController;
            bossController.OnCheckShotPosition += CheckInstallation;
        }
    }

    // 물풍선 이벤트 등록
    public void GetBalloonController(Vector2 pos, BalloonController controller)
    { 
        stageMap.GetPosition(pos).balloon = controller;

        controller.OnReady += CheckPopDirection;
        controller.OnBalloonDestroyed += DestroyStageObject;
    }

    public void SetStageObject(ObjectTypeEnums type, Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        // Debug.Log($"Setting object at map[{x},{y}] to {type}");

        stageMap.SetStageObjcet(type, x, y);

        // Debug.Log($"[After Setting] Object type at ({x},{-y}): {stageMap.GetPosition(new Vector2(x, -y)).ObjectType}");

        // Debug.Log("[" + type + "]" + x + ", " + -y);
    }

    public void SetStageEnemy(ObjectTypeEnums type, Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.SetStageEnemy(type, x, y);
    }

    public void DestroyStageObject(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        // Debug.Log(pos + " 삭제 중");

        stageMap.DestroyStageObject(x, y);
    }
    
    public void RemoveEnemy(Vector2 pos, EnemyController enemy)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.DestroyStageObject(x, y);

        stageEnemy.RemoveEnemy(enemy);
    }

    public void RemoveBox(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageBlock.RemoveBox(pos);
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

        if(x < 0 || x >= 15 || y > 0 || y <= -13) return ObjectTypeEnums.BORDER;

        return stageMap.CheckObjectType(x, y);
    }

    public void CheckPopDirection(BalloonController balloon, Vector2 pos, int power)
    {
        count = 0;
        balloonPopList.Add(balloon);

        // 4방향 검사 후 생성할 위치 저장 (시계방향으로 검사)
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 direction = directions[i];

            for (int j = 1; j <= power; j++)
            {
                Vector2 spawnPosition = pos + direction * j;
                
                // 위치 체크 후 필요한 경우 리스트에 추가
                if (CheckPosition(spawnPosition, balloon.isBoss)) 
                {
                    balloon.streamList[i].Add(spawnPosition);
                }
                else
                {
                    break;
                }
            }
        }

        if(count == 0)
        {
            PopBalloons();
        }
    }

    private void PopBalloons()
    {
        foreach(BalloonController pop in balloonPopList)
        {
            stageMap.GetPosition(pop.transform.position).balloon = null;
            pop.StartChangeState(BalloonStateEnums.POP);
        }

        foreach(BalloonController pop in balloonPopList)
        {
            ChangeObjectType(pop.transform.position, ObjectTypeEnums.NONE);
        }
        
        balloonPopList.Clear();
    }

    private bool CheckPosition(Vector2 position, bool isboss = false)
    {
        ObjectTypeEnums type = CheckObjectType(position);
        // Debug.Log("Object type at position " + position + ": " + type);
    
        if (type == ObjectTypeEnums.OBJECT || type == ObjectTypeEnums.BORDER) 
        {
            return false;
        }
        else if (type == ObjectTypeEnums.BOX)
        {
            RemoveBox(position);
            return false;
        }
        else if(type == ObjectTypeEnums.BALLOON)
        {
            // Debug.Log(position + " : " +stageMap.GetPosition(position).balloon);
            if(!stageMap.GetPosition(position).balloon.stateMachine.CheckCurState(BalloonStateEnums.READY) &&
                isboss == stageMap.GetPosition(position).balloon.isBoss)
            {
                stageMap.GetPosition(position).balloon.stateMachine.ChangeState(BalloonStateEnums.READY);
                count ++;          
            }
            return true;
        }
        else if (type == ObjectTypeEnums.NONE)
        {
            return true;
        }
        return true;
    }

    public void ChangeObjectType(Vector2 pos, ObjectTypeEnums type)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.ChangeObjectType(type, x, y);
    }

    public Vector2 GetPlayerDirection()
    {
        return playerController.GetDirection();
    }
}
