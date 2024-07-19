using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHander;
    public PlayerController controller;
    public PlayerStateMachine stateMachine;

    void Start()
    {
        inputHander.OnPlayerIdle += ChangeIdleState;
        inputHander.OnPlayerMove += ChangeMoveState;
        // inputHander.OnCheckHorizontal += CheckHorizontal;
    }

    public void ChangeIdleState()
    {
        stateMachine.ChangeInputState(PlayerStateEnums.IDLE);
    }

    public void ChangeMoveState()
    {
        stateMachine.ChangeInputState(PlayerStateEnums.MOVE);
    }
}