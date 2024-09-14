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
        
        if(bossController.curAttack.attackType == BossAttackTypeEnums.SHOOT)
        {
            bossController.ShootAttack(bossController.curAttack.attackDirection[bossController.curAttack.attackDirection.Length - bossController.curAttack.attackCount]);
        }
        else if(bossController.curAttack.attackType == BossAttackTypeEnums.HOOP)
        {
            bossController.attackCoroutine = bossController.StartCoroutine(bossController.HoopAttack());
        }
    }

    public void OnExit()
    {
        bossController.curAttack.attackCount--;
        bossController.StopCoroutine(bossController.attackCoroutine);
    }
}
