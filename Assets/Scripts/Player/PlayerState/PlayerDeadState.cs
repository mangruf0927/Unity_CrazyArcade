using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    public PlayerController playerController { get; set; }
    public PlayerStateMachine stateMachine { get; set; }

    // 생성자의 주요 목적은 객체를 초기화하고 초기 상태로 설정하는 것
    public PlayerDeadState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        playerController = stateMachine.playerController;
    }

    public HashSet<PlayerStateEnums> inputHash { get; } = new HashSet<PlayerStateEnums>()
    {
    };

    public HashSet<PlayerStateEnums> logicHash { get; } = new HashSet<PlayerStateEnums>()
    {
    };

    
    public void Update()
    {
    }
    
    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        SoundManager.Instance.PlaySFX("PlayerDie");

        playerController.animator.Play("Dead");
        playerController.rigid.velocity = Vector2.zero;
        
        playerController.StartCoroutine(playerController.DestroyPlayer());
    }

    public void OnExit()
    {
    }
}

