using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossAttackState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        bossController = stateMachine.bossController;
    }
    
    public void Update()
    {
        bossController.PlayAttackAnimation();
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        bossController.rigid.velocity = Vector2.zero;
        bossController.ShootAttack();
    }

    public void OnExit()
    {

    }
}
