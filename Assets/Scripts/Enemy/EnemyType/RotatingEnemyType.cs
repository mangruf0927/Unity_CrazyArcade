using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemyType : EnemyType
{
    public override void RoamUpdate()
    {
        enemyController.PlayMoveAnimation();
        CheckForPlayer();
        CheckForObstacle();
    }

    public override void RoamFixedUpdate()
    {
        enemyController.Move();
    }

    public override void RoamOnEnter()
    {
        StartCoroutine(SpeedUp());
    }

    public override void RoamOnExit()
    {

    }

    // >> 
    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(60f);
        enemyController.moveSpeed *= 1.5f;
    }

    private bool isClockwise = true;

    public void CheckForPlayer()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        if (enemyController.isPlayerDead) return;

        foreach (Vector2 direction in directions)
        {
            // 현재 이동 중인 방향은 제외
            if (direction == enemyController.moveDirection) continue;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, enemyController.sensingRange, LayerMask.GetMask("Player"));

            // 플레이어를 감지한 경우
            if (hit.collider != null)
            {
                if (direction == -enemyController.moveDirection) // 반대 방향에 플레이어가 있을 때
                {
                    isClockwise = !isClockwise; // 시계/반시계 방향 전환
                    enemyController.moveDirection = direction;
                }
                else
                {
                    enemyController.stateMachine.ChangeState(EnemyStateEnums.MOVE);
                    enemyController.moveDirection = direction;
                }
                break; // 한 번 감지되면 다른 방향은 검사하지 않음
            }
        }
    }

    public void CheckForObstacle()
    {
        int obstacleLayer = LayerMask.GetMask("Obstacle");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, enemyController.boxSize, 0f, 
                            enemyController.moveDirection, enemyController.rayDistance, obstacleLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                SetDirection();
                break; 
            }
        }
    }

    public void SetDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> availableDirections = new List<Vector2>(directions);

        // 현재 방향을 제외한 나머지 방향 체크
        foreach (Vector2 direction in directions)
        {
            int obstacleLayer = LayerMask.GetMask("Obstacle");
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, enemyController.rayDistance, obstacleLayer);

            foreach (RaycastHit2D hit in hits)
            {
                // 장애물이 있으면 방향 제거
                availableDirections.Remove(direction);
            }
        }

        // 시계방향 우선
        Vector2 nextDirection = GetClockwiseDirection(enemyController.moveDirection, isClockwise); 
        if (availableDirections.Contains(nextDirection))
        {
            enemyController.moveDirection = nextDirection;
            enemyController.isConfined = false;
            return;
        }
    
        // 남은 방향 중에서 랜덤으로 선택 (80%는 반대 방향, 20%는 나머지)
        if (availableDirections.Count > 0)
        {
            if (availableDirections.Count == 1)
            {
                enemyController.moveDirection = availableDirections[0];
                enemyController.isConfined = false;
            }
            else
            {
                // 반대 방향이 포함된 경우
                if (availableDirections.Contains(-enemyController.moveDirection))
                {
                    if (Random.value < 0.8f) // 80% 확률
                    {
                        enemyController.moveDirection = -enemyController.moveDirection;
                        enemyController.isConfined = false;
            
                        // 시계 -> 반시계 / 반시계 -> 시계
                        isClockwise = !isClockwise;
                        return;

                    }
                    else // 20% 확률
                    {
                        availableDirections.Remove(-enemyController.moveDirection);
                        enemyController.moveDirection = availableDirections[0];
                        enemyController.stateMachine.ChangeState(EnemyStateEnums.MOVE);
                        return;
                    }
                }
            }
        }
        else
        {
            enemyController.isConfined = true; // 갈 수 있는 방향이 없으면 confined 상태로 설정
        }
    }

    private Vector2 GetClockwiseDirection(Vector2 currentDirection, bool clockwise)
    {
        if (clockwise)
        {
            if (currentDirection == Vector2.up) return Vector2.right;
            if (currentDirection == Vector2.right) return Vector2.down;
            if (currentDirection == Vector2.down) return Vector2.left;
            if (currentDirection == Vector2.left) return Vector2.up;
        }
        else
        {
            if (currentDirection == Vector2.up) return Vector2.left;
            if (currentDirection == Vector2.left) return Vector2.down;
            if (currentDirection == Vector2.down) return Vector2.right;
            if (currentDirection == Vector2.right) return Vector2.up;
        }

        return currentDirection; // 기본적으로 동일한 방향 반환
    }

}
