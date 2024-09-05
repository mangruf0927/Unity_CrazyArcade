using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleAttackState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    private float time;

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
    }

    public void OnEnter()
    {
        time = 0;
        bossController.animator.Play("Idle");
    }

    public void OnExit()
    {

    }
}

