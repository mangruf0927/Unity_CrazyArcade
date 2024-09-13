using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossDeadState(BossStateMachine _stateMachine)
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
        bossController.animator.Play("Dead");
        bossController.StartCoroutine(bossController.DestroyBoss());
    }

    public void OnExit()
    {
        
    }
}