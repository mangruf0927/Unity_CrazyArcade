using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossHitState(BossStateMachine _stateMachine)
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
        bossController.DamgeUp();
        bossController.PlayHitAnimation();
        bossController.StartCoroutine(bossController.ChangeStateAfterAnimation(BossStateEnums.WAIT));
    }

    public void OnExit()
    {
        bossController.isHit = false;
    }
}

