using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("리지드 바디")]
    public Rigidbody2D rigid;

    [Header("애니메이터")]
    public Animator animator;

    [Header("플레이어 상태머신")]
    public PlayerStateMachine stateMachine;

    [Header("이동 속도")]
    public float speed;

    [Header("물풍선에 갇혔을 때 이동 속도")]
    public float trapSpeed;

    [Header("최대 풍선 개수")]
    public int maxBalloonNum;
    private int balloonNum; // 현재 풍선 개수

    private void Update()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }
}