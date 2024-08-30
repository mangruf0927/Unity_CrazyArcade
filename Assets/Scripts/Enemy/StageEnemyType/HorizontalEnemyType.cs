using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalEnemyType : StageEnemyType
{
    public override void RoamUpdate()
    {
        enemy.PlayMoveAnimation();
        CheckForObstacle();
        ChangeDirection();
    }

    public override void RoamFixedUpdate()
    {
        enemy.Move();
    }

    public override void RoamOnEnter()
    {
        
    }

    public override void RoamOnExit()
    {

    }

    // >> 
    private bool isWaiting = false;

    private void CheckForObstacle()
    {
        int obstacleLayer = LayerMask.GetMask("Obstacle");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, enemy.boxSize, 0f, enemy.moveDirection, enemy.rayDistance, obstacleLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                StartCoroutine(ChangeStateWithDelay());
                //enemy.stateMachine.ChangeState(EnemyStateEnums.MOVE); 
                break; // 첫 번째 장애물에 충돌하면 루프 종료
            }
        }
    }

    private IEnumerator ChangeStateWithDelay()
    {
        enemy.isConfined = true;
        yield return new WaitForSeconds(0.3f);
        enemy.isConfined = false;
        enemy.stateMachine.ChangeState(EnemyStateEnums.MOVE); 
    }

    private void ChangeDirection()
    {
        if (!isWaiting && (transform.position.x < 1 || transform.position.x > 13)) // 경계에 도달했을 때
        {
            StartCoroutine(ChangeDirectionWithDelay()); 
        }
    }

    private IEnumerator ChangeDirectionWithDelay()
    {
        isWaiting = true;
        enemy.isConfined = true;
        yield return new WaitForSeconds(0.5f); // 1초 대기

        enemy.moveDirection = -enemy.moveDirection; 
        enemy.isConfined = false;

        // 적이 일정 거리 이상 이동할 때까지 대기
        float initialPositionX = transform.position.x;
        while (Mathf.Abs(transform.position.x - initialPositionX) < 0.5f)
        {
            yield return null;
        }
        isWaiting = false;
    }
    
}
