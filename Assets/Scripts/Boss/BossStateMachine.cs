using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public BossController bossController;

    [HideInInspector] public IBossState curState;
    public Dictionary<BossStateEnums, IBossState> stateDictionary;

    private void Awake() 
    {
        stateDictionary = new Dictionary<BossStateEnums, IBossState>
        {

        };

        if(stateDictionary.TryGetValue(BossStateEnums.IDLE, out IBossState newState))
        {
            curState = newState;
            newState.OnEnter();
        }
    }

    public void ChangeState(BossStateEnums newStateType)
    {
        if(curState == null) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IBossState newState))
        {
            curState = newState;
            curState.OnExit();
        }
    }
}
