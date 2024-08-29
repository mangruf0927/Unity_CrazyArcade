using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamState : IEnemyState
{
    public EnemyController enemyController{get; set;}
    public EnemyStateMachine stateMachine{get; set;}

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public EnemyRoamState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        enemyController = stateMachine.enemyController;
    }


    public void Update()
    {
        enemyController.enemyType.RoamUpdate();
    }

    public void FixedUpdate()
    {
        enemyController.enemyType.RoamFixedUpdate();
    }

    public void OnEnter()
    {
        enemyController.enemyType.RoamOnEnter();
    }

    public void OnExit()
    {
        enemyController.enemyType.RoamOnExit();
    }
}