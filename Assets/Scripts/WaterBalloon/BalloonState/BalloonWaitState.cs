using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonWaitState : IBalloonState
{
    public BalloonController balloonController{get; set;}
    public BalloonStateMachine stateMachine{get; set;}

    public BalloonWaitState(BalloonStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        balloonController = stateMachine.balloonController;
    }

    public void Update()
    {

    }
    float time;
    public void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        if (time >= 2.5f)
        {
            stateMachine.ChangeState(BalloonStateEnums.READY);
        }
    }

    public void OnEnter()
    {
        time = 0;
        // balloonController.StartChangeState(BalloonStateEnums.READY, 2.5f);
    }

    public void OnExit()
    {
        balloonController.balloonCollider.isTrigger = true;
        balloonController.DestroyObject();
    }
}
