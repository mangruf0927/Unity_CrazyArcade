using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHandler ;
    public PlayerController controller;
    public PlayerStateMachine stateMachine;

    void Start()
    {
        inputHandler.OnPlayerIdle += ChangeIdleState;
        inputHandler.OnPlayerMove += ChangeMoveState;
        inputHandler.OnWaterBalloonSet += SetWaterBalloon;
        
        inputHandler.OnCheckHorizontal += CheckHorizontal;
        inputHandler.OnCheckDirection += CheckDirection;
    }

    public void ChangeIdleState()
    {
        stateMachine.ChangeInputState(PlayerStateEnums.IDLE);
    }

    public void ChangeMoveState()
    {
        stateMachine.ChangeInputState(PlayerStateEnums.MOVE);
    }

    public void CheckHorizontal(bool isTrue)
    {
        controller.isHorizontal = isTrue;
    }

    public void CheckDirection(Vector2 direction)
    {
        controller.SetDirection(direction);
    }

    public void SetWaterBalloon()
    {
        if(stateMachine.curState is PlayerIdleState || stateMachine.curState is PlayerMoveState)
        {
            controller.SetWaterBalloon();
        }
    }
}