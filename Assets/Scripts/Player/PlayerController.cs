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

    [Header("이동 방향")]
    public Vector3 moveDirection;
    public bool isHorizontal;

    [Header("물풍선 프리팹")]
    public GameObject balloonPrefab;

    [Header("물풍선 개수")]
    public int balloonNum;

    private void Update()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();

        Debug.Log(stateMachine.curState);
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }

    public void Move()
    {
        Vector2 moveVector = isHorizontal ? new Vector2(moveDirection.x, 0) : new Vector2(0, moveDirection.y);
        rigid.velocity = moveVector * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void PlayMoveAnimation()
    {
        if (isHorizontal)
        {
            if (moveDirection.x > 0)
            {
                animator.Play("Move_Right");
            }
            else if (moveDirection.x < 0)
            {
                animator.Play("Move_Left");
            }
        }
        else
        {
            if(moveDirection.y > 0)
            {
                animator.Play("Move_Up");
            }
            else if(moveDirection.y < 0)
            {
                animator.Play("Move_Down");
            }
        }
    }

    public void SetWaterBalloon()
    {
        if(balloonNum > 0)
        {
            balloonNum -= 1;

            GameObject waterBalloon = Instantiate<GameObject>(balloonPrefab);
            waterBalloon.transform.position = rigid.position;
        }
    }
}