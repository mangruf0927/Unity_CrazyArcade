using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController playerController { get; set; }
    public PlayerStateMachine stateMachine { get; set; }

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerIdleState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        playerController = stateMachine.playerController;
    }

    public HashSet<PlayerStateEnums> inputHash { get; } = new HashSet<PlayerStateEnums>()
    {
        PlayerStateEnums.MOVE,
    };

    public HashSet<PlayerStateEnums> logicHash { get; } = new HashSet<PlayerStateEnums>()
    {
        PlayerStateEnums.TRAP,
    };

    
    public void Update()
    {
        if(playerController.CheckTrap())
        {
            Debug.Log("키키키");
            stateMachine.ChangeLogicState(PlayerStateEnums.TRAP);
        }
    }
    
    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        playerController.animator.Play("Idle");
        playerController.rigid.velocity = Vector2.zero;
    }

    public void OnExit()
    {

    }
}
