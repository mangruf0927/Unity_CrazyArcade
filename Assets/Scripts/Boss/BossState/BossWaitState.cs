using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitState : IBossState
{
    public BossController bossController{get; set;}
    public BossStateMachine stateMachine{get; set;}

    public BossWaitState(BossStateMachine _stateMachine)
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
        bossController.StartCoroutine(ChangeBossState());
    }

    public void OnExit()
    {
    }

    private IEnumerator ChangeBossState()
    {
        yield return new WaitForSeconds(0.05f); 

        if(bossController.curAttack.attackCount > 0)
        {
            bossController.stateMachine.ChangeState(BossStateEnums.ATTACK);
        }
        else
        {
            bossController.stateMachine.ChangeState(BossStateEnums.MOVE);
        }
    }
}


