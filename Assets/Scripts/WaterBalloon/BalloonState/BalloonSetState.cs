using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSetState : IBalloonState
{
    public BalloonController balloonController{get; set;}
    public BalloonStateMachine stateMachine{get; set;}

    public BalloonSetState(BalloonStateMachine _stateMachine)
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
        GameObject waterBalloon;

        waterBalloon = Object.Instantiate<GameObject>(balloonController.balloonPrefab, balloonController.transform);

        waterBalloon.transform.position = balloonController.transform.position;
        balloonController.currentWaterBalloon = waterBalloon;

        stateMachine.ChangeState(BalloonStateEnums.WAIT);
    }

    public void OnExit()
    {
    }
}
