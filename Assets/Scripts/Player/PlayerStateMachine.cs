using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerController playerController;

    [HideInInspector] public IPlayerState curState; // 현재 상태
    public Dictionary<PlayerStateEnums, IPlayerState> stateDictionary;

    private void Start() 
    {
        stateDictionary = new Dictionary<PlayerStateEnums, IPlayerState>
        {
            { PlayerStateEnums.READY, new PlayerReadyState(this) },
            { PlayerStateEnums.IDLE, new PlayerIdleState(this) },
            { PlayerStateEnums.MOVE, new PlayerMoveState(this) },
            { PlayerStateEnums.TRAP, new PlayerTrapState(this) },
            { PlayerStateEnums.DEAD, new PlayerDeadState(this) },
            { PlayerStateEnums.CLEAR, new PlayerClearState(this) },
        };

        if(stateDictionary.TryGetValue(PlayerStateEnums.READY, out IPlayerState newState))
        {
            // TryGetValue : Key가 있는지 확인과 동시에 Value 값 반환

            curState = newState;
            curState.OnEnter();
        }
    }

    public void ChangeInputState(PlayerStateEnums newStateType) // 입력 받았을 때 상태 변경
    {
        if(curState == null) return;
        if(!curState.inputHash.Contains(newStateType)) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public void ChangeLogicState(PlayerStateEnums newStateType) // 키 입력이 아닌 스스로 상태 변경
    {
        if(curState == null) return;
        if(!curState.logicHash.Contains(newStateType)) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public void ChangeState(PlayerStateEnums newStateType) 
    {
        if(curState == null) return;
        if(CheckCurState(newStateType)) return;

        curState.OnExit();

        if(stateDictionary.TryGetValue(newStateType, out IPlayerState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public bool CheckCurState(PlayerStateEnums newStateType)
    {
        if (stateDictionary.TryGetValue(newStateType, out IPlayerState stateValue))
        {
            return curState == stateValue;
        }
        return false; // 키가 존재하지 않는 경우 false 반환
    }
}
