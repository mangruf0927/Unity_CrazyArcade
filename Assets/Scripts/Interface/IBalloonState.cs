using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBalloonState
{
    BalloonController balloonController{get; set;}
    BalloonStateMachine stateMachine{get; set;}

    void Update();
    void FixedUpdate();
    void OnEnter();
    void OnExit();
} 
