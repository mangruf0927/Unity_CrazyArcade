using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrapState : IPlayerState
{
    public PlayerController playerController { get; set; }
    public PlayerStateMachine stateMachine { get; set; }

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerTrapState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        playerController = stateMachine.playerController;
    }

    public HashSet<PlayerStateEnums> inputHash { get; } = new HashSet<PlayerStateEnums>()
    {
    };

    public HashSet<PlayerStateEnums> logicHash { get; } = new HashSet<PlayerStateEnums>()
    {
        PlayerStateEnums.DEAD,
        PlayerStateEnums.CLEAR
    };

    
    public void Update()
    {
        if(playerController.CheckClear())
        {
            stateMachine.ChangeLogicState(PlayerStateEnums.CLEAR);
        }
    }
    
    public void FixedUpdate()
    {
        playerController.Move(0.3f);
    }

    public void OnEnter()
    {
        SoundManager.Instance.PlaySFX("PlayerTrap");
        
        playerController.animator.Play("Trap");
        playerController.StartChangeState(PlayerStateEnums.DEAD);
    }

    public void OnExit()
    {
    }
}