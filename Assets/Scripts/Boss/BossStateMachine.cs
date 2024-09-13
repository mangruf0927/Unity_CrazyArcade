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
            {BossStateEnums.IDLEATTACK, new BossIdleAttackState(this)},
            {BossStateEnums.MOVE, new BossMoveState(this)},
            {BossStateEnums.ATTACK, new BossAttackState(this)},
            {BossStateEnums.WAIT, new BossWaitState(this)},
            {BossStateEnums.HIT, new BossHitState(this)},
            {BossStateEnums.TRAP, new BossTrapState(this)},
            {BossStateEnums.ANGRY, new BossAngryState(this)},
            {BossStateEnums.DEAD, new BossDeadState(this)}
        };

        if(stateDictionary.TryGetValue(BossStateEnums.MOVE, out IBossState newState))
        {
            curState = newState;
            newState.OnEnter();
        }
    }

    public void ChangeState(BossStateEnums newStateType)
    {
        if (curState == null) return;
        if (CheckCurState(newStateType)) return;

        curState.OnExit();

        if (stateDictionary.TryGetValue(newStateType, out IBossState newState))
        {
            curState = newState;
            curState.OnEnter();
        }
    }

    public bool CheckCurState(BossStateEnums newStateType)
    {
        if (stateDictionary.TryGetValue(newStateType, out IBossState stateValue))
        {
            return curState == stateValue;
        }
        return false; // 키가 존재하지 않는 경우 false 반환
    }
}
