using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonReadyState : IBalloonState
{
    public BalloonController balloonController{get; set;}
    public BalloonStateMachine stateMachine{get; set;}

    public BalloonReadyState(BalloonStateMachine _stateMachine)
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
        Debug.Log("Ready");
        balloonController.CheckForReady();
    }

    public void OnExit()
    {
    }
}

