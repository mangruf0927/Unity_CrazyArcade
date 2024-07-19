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

        };

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
}