using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossWinState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        bossController = stateMachine.bossController;
    }
    
    public void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {   
        bossController.rigid.velocity = Vector2.zero;
        bossController.animator.Play("Win");
    }

    public void OnExit()
    {
    }
}


