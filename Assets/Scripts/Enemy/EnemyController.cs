using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy 상태머신")]
    public EnemyStateMachine stateMachine;

    private void Update() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();

        // Debug.Log("Enemy상태 " + stateMachine.curState);
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }
}
