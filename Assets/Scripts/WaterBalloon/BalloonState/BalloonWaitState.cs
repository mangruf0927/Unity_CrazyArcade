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

    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        balloonController.StartChangeState(2.5f, BalloonStateEnums.POP);
    }

    public void OnExit()
    {
        balloonController.DestroyObject();
    }
}