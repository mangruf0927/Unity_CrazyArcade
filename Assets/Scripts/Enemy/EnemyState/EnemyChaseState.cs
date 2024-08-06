using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public EnemyController enemyController{get; set;}
    public EnemyStateMachine stateMachine{get; set;}

    public EnemyChaseState(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        enemyController = stateMachine.enemyController;
    }


    public void Update()
    {
    }

    public void FixedUpdate()
    {
        enemyController.Move();
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
