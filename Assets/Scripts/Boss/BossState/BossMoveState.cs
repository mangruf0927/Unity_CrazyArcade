using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossMoveState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        bossController = stateMachine.bossController;
    }
    
    public void Update()
    {
        bossController.PlayMoveAnimation();
    }

    public void FixedUpdate()
    {
        bossController.Move();
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}

