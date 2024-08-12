using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStateMachine : MonoBehaviour
{
    public BalloonController balloonController;

    [HideInInspector] public IBalloonState curState;
    public Dictionary<BalloonStateEnums, IBalloonState> stateDictionary;
    
    private void Awake() 
    {
        stateDictionary = new Dictionary<BalloonStateEnums, IBalloonState>
        {
            { BalloonStateEnums.SET, new BalloonSetState(this) }, // this : 현재 인스턴스를 참조하는 것
            { BalloonStateEnums.WAIT, new BalloonWaitState(this) }, 
            { BalloonStateEnums.READY, new BalloonReadyState(this) }, 
            { BalloonStateEnums.POP, new BalloonPopState(this) }, 
            { BalloonStateEnums.DESTROY, new BalloonDestroyState(this) }, 
        };
    }

    private void Start() 
    {
        if(stateDictionary.TryGetValue(BalloonStateEnums.SET, out IBalloonState newState))
        {
            curState = newState;
            curState.OnEnter();
        }    
    }

    public void ChangeState(BalloonStateEnums newStateType)
    {
        if(curState == null) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IBalloonState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public bool CheckCurState(BalloonStateEnums newStateType)
    {
        if (stateDictionary.TryGetValue(newStateType, out IBalloonState stateValue))
        {
            return curState == stateValue;
        }
        return false; // 키가 존재하지 않는 경우 false 반환
    }
}
