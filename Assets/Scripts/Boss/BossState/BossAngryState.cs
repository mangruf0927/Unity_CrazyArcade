using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FFAFAF

public class BossAngryState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossAngryState(BossStateMachine _stateMachine)
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
        bossController.isHit = true;
        bossController.rigid.velocity = Vector2.zero;
        bossController.StartCoroutine(bossController.Angry());
    }

    public void OnExit()
    {
        bossController.isHit = false;
    }
}