using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrapState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossTrapState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        bossController = stateMachine.bossController;
    }
    
    public void Update()
    {
        Debug.Log("Trap");
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        bossController.animator.Play("Trap");
        bossController.TrapBoss();
        bossController.StartCoroutine(bossController.ChangeStateAfterAnimation(BossStateEnums.DEAD));
    }

    public void OnExit()
    {

    }
}