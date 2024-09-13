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
        if (bossController.stat.currentHP <= 0)
        {
            bossController.stateMachine.ChangeState(BossStateEnums.TRAP);
            Debug.Log("ggg " + bossController.stat.currentHP);
            return;
        }
        if(bossController.stat.currentHP == 30)
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
        bossController.stat.GetDamage();
        bossController.PlayHitAnimation();
    }

    public void OnExit()
    {
       if (bossController.stat.currentHP > 0)
       {
            bossController.isHit = false;
            // Debug.Log(bossController.isHit);
       }
    }
}

