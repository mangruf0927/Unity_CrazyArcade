using System.Collections;
using System.Collections.Generic;
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
    private void CheckForObstacle()
    {
        int obstacleLayer = LayerMask.GetMask("Obstacle");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, enemy.boxSize, 0f, enemy.moveDirection, enemy.rayDistance, obstacleLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                enemy.stateMachine.ChangeState(EnemyStateEnums.MOVE); 
                // ChangeStateAfterDelay(1f);
                break; // 첫 번째 장애물에 충돌하면 루프 종료
            }
        }
    }

    private IEnumerator ChangeStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        enemy.stateMachine.ChangeState(EnemyStateEnums.MOVE); 
    }

    private void ChangeDirection()
    {
        if (transform.position.x < 1 || transform.position.x > 13)
        {
            enemy.moveDirection = -enemy.moveDirection; 
        }
    }

    private IEnumerator ChangeDirectionAfterDelay(float delay)
    {
        enemy.isConfined = true;
        yield return new WaitForSeconds(delay); 
        enemy.moveDirection = -enemy.moveDirection; 
        enemy.isConfined = false;
    }

    
}
