using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyController enemyController;

    [HideInInspector] public IEnemyState curState;
    public Dictionary<EnemyStateEnums, IEnemyState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<EnemyStateEnums, IEnemyState>
        {
            {EnemyStateEnums.READY, new EnemyReadyState(this)},
            {EnemyStateEnums.ROAM, new EnemyRoamState(this)},
            {EnemyStateEnums.MOVE, new EnemyMoveState(this)},
            {EnemyStateEnums.DEAD, new EnemyDeadState(this)},
            {EnemyStateEnums.GAMEOVER, new EnemyGameOverState(this)},
        };

        if(stateDictionary.TryGetValue(EnemyStateEnums.READY, out IEnemyState newState))
        {
            curState = newState;
            newState.OnEnter(); 
        }
    }

    public void ChangeState(EnemyStateEnums newStateType)
    {
        if(curState == null) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IEnemyState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }
}
