using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSettingCenter : MonoBehaviour
{
    public StageBlock stageBlock;
    public StageMap stageMap;
    public PlayerController player;
    
    public List<EnemyController> enemyList;

    private List<BalloonController> balloonPopList = new List<BalloonController>();
    private int count;


    private void Awake() 
    {
        stageBlock.OnBlockInstall += SetStageObject;

        foreach(MovableBox box in stageBlock.MovableBoxeList)
        {
            box.OnGetDirection += GetPlayerDirection;
            box.OnMoveCheck += CheckObjectType;
            box.OnRegisterNewPos += SetStageObject;
            box.OnRemoveOriginPos += DestroyStageObject;
        }

        foreach(EnemyController enemy in enemyList)
        {
            enemy.OnUpdatePosition += SetStageEnemy;
            enemy.OnRemovePosition += DestroyStageObject;
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

    public void CheckFourWays(BalloonController balloon, Vector2 pos, int power)
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
                //Debug.Log(spawnPosition);
                
                // 위치 체크 후 필요한 경우 리스트에 추가
                if (CheckPosition(spawnPosition)) 
                {
                    break;
                }
                else
                {
                    balloon.streamList[i].Add(spawnPosition);
                }
            }
        }

        if(count == 0)
        {
            foreach(BalloonController pop in balloonPopList)
            {
                stageMap.Return(pop.transform.position).balloon = null;
                pop.StartChangeState(BalloonStateEnums.POP);
            }

            foreach(BalloonController pop in balloonPopList)
            {
                ChangeObjectType(pop.transform.position, ObjectTypeEnums.None);
            }
            
            balloonPopList.Clear();
        }
    }

        private bool CheckPosition(Vector2 position)
    {
        ObjectTypeEnums type = CheckObjectType(position);
    
        if (type == ObjectTypeEnums.Object) 
        {
            return true;
        }
        else if (type == ObjectTypeEnums.Box)
        {
            RemoveBox(position);
            return true;
        }
        else if(type == ObjectTypeEnums.Balloon)
        {
            if(!stageMap.Return(position).balloon.stateMachine.CheckCurState(BalloonStateEnums.READY))
            {
                stageMap.Return(position).balloon.stateMachine.ChangeState(BalloonStateEnums.READY);
                count ++;
            }
            return false;
        }
        else if (type == ObjectTypeEnums.None)
        {
            return false;
        }
        return false;
    }

    public void ChangeObjectType(Vector2 pos, ObjectTypeEnums type)
    {
        int x = Mathf.FloorToInt(pos.x); 
        int y = Mathf.FloorToInt(pos.y);

        stageMap.ChangeObjectType(type, x, y);
    }

    public Vector2 GetPlayerDirection()
    {
        return player.GetDirection();
    }

    public void GetBalloonController(Vector2 pos, BalloonController controller)
    { 
        stageMap.Return(pos).balloon = controller;

        controller.OnReady += CheckFourWays;
        controller.OnBalloonDestroyed += DestroyStageObject;
    }
}
