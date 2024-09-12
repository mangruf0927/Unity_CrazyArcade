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

    Coroutine coroutine;
    
    public void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        if (bossController == null)
        {
            Debug.LogError("bossController is null in BossWaitState!");
            return;
        }

        coroutine = bossController.StartCoroutine(ChangeBossState());
    }

    public void OnExit()
    {
        if(coroutine != null)
            bossController.StopCoroutine(coroutine);
    }

    private IEnumerator ChangeBossState()
    {
        yield return new WaitForSeconds(0.05f); 

        if (bossController.curAttack == null)
        {
            bossController.stateMachine.ChangeState(BossStateEnums.MOVE);
            yield break;
        }

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


