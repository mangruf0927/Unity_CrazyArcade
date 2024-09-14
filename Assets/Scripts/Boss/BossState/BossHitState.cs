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
    
    float time;

    public void Update()
    {
        if (bossController.hitScan.currentHP <= 0)
        {
            bossController.stateMachine.ChangeState(BossStateEnums.TRAP);
            Debug.Log("ggg " + bossController.hitScan.currentHP);
            return;
        }
        if(bossController.hitScan.currentHP == 30)
        {
            bossController.stateMachine.ChangeState(BossStateEnums.ANGRY);
            return;
        }
        if(bossController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f &&
            bossController.animator.GetCurrentAnimatorStateInfo(0).IsName(bossController.curAniClip))
        {
            time += Time.deltaTime;

            if(time > 0.5f)
            {
                // bossController.isHit = false;
                bossController.stateMachine.ChangeState(BossStateEnums.WAIT);
                return;
            }
        }
    }

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {   
        time = 0;

        bossController.rigid.velocity = Vector2.zero;
        bossController.hitScan.GetDamage();
        bossController.PlayHitAnimation();
    }

    public void OnExit()
    {
       if (bossController.hitScan.currentHP > 0)
       {
            bossController.isHit = false;
            // Debug.Log(bossController.isHit);
       }
    }
}

