using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopState : IBalloonState
{
    public BalloonController balloonController{get; set;}
    public BalloonStateMachine stateMachine{get; set;}

    public BalloonPopState(BalloonStateMachine _stateMachine)
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
        balloonController.Explode();
    }

    public void OnExit()
    {
    }
}
