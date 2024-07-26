using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public PlayerController playerController { get; set; }
    public PlayerStateMachine stateMachine { get; set; }

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerMoveState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        playerController = stateMachine.playerController;
    }

    public HashSet<PlayerStateEnums> inputHash { get; } = new HashSet<PlayerStateEnums>()
    {
        PlayerStateEnums.IDLE,
    };

    public HashSet<PlayerStateEnums> logicHash { get; } = new HashSet<PlayerStateEnums>()
    {
        PlayerStateEnums.TRAP,
    };

    
    public void Update()
    {
        playerController.PlayMoveAnimation();

        if(playerController.CheckTrap())
        {
            stateMachine.ChangeLogicState(PlayerStateEnums.TRAP);
        }
    }
    
    public void FixedUpdate()
    {
        playerController.Move(playerController.stat.moveSpeed);
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
    }
}