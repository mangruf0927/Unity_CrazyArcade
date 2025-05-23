using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReadyState : IEnemyState
{
    public EnemyController enemyController{get; set;}
    public EnemyStateMachine stateMachine{get; set;}

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public EnemyReadyState(EnemyStateMachine _stateMachine)
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
        enemyController.animator.Play("Ready");
        enemyController.StartChangeState(EnemyStateEnums.ROAM, 1.5f);
    }

    public void OnExit()
    {

    }
}

