using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleAttackState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    private float time;
    private float lastSpawnTime;

    public BossIdleAttackState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        bossController = stateMachine.bossController;
    }
    
    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if (time >= 30f)
        {
            stateMachine.ChangeState(BossStateEnums.MOVE);
        }
        // 풍선을 3초마다 생성
        if (time - lastSpawnTime >= 1.2f)
        {
            bossController.IdleAttack();
            lastSpawnTime = time; // 마지막 풍선 생성 시간을 업데이트
        }
    }

    public void OnEnter()
    {
        time = 0;
        lastSpawnTime = 0;

        bossController.animator.Play("Idle");
    }

    public void OnExit()
    {

    }
}

