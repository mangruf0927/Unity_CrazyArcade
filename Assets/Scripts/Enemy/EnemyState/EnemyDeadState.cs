using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    public EnemyController enemyController{get; set;}
    public EnemyStateMachine stateMachine{get; set;}

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public EnemyDeadState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        enemyController = stateMachine.enemyController;
    }


    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }

    public void OnEnter()
    {
        enemyController.rigid.velocity = Vector2.zero;
        enemyController.animator.Play("Dead");
        enemyController.StartDestroyEnemy();
    }

    public void OnExit()
    {

    }
}
