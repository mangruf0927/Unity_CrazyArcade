using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDestroyState : IBalloonState
{
    public BalloonController balloonController{get; set;}
    public BalloonStateMachine stateMachine{get; set;}

    public BalloonDestroyState(BalloonStateMachine _stateMachine)
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
        balloonController.DestroyWaterBalloon();
    }

    public void OnExit()
    {

    }
}