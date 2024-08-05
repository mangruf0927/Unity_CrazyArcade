using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy 상태머신")]
    public EnemyStateMachine stateMachine;

    [Header("리지드 바디")]
    public Rigidbody2D rigid;

    [Header("애니메이터")]
    public Animator animator;

    [Header("스프라이트")]
    public SpriteRenderer spriteRenderer;

    [Header("Enemy 속도")]
    public float enemySpeed;

    [Header("이동 방향")]
    public Vector2 moveDirection;

    private bool isConfined = false;

    private float rayDistance = 0.6f;

    private void Update() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();

        Debug.Log("Enemy상태 " + stateMachine.curState);
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }

    // >> :
    public void Move()
    {
        if(!isConfined)
            rigid.velocity = moveDirection * enemySpeed;
        else
            rigid.velocity = Vector2.zero;
    }

    public void CheckForObstacle()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDirection, rayDistance);
    
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                // Debug.Log(hit.collider + " 와 충돌");
                SetDirection();
                break; // 첫 번째 장애물에 충돌하면 루프 종료
            }
        }
    }

    public void SetDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> availableDirections = new List<Vector2>{ Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        
        // 현재 방향을 제외한 나머지 방향 체크
        foreach (Vector2 direction in directions)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, rayDistance);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Obstacle")) // 장애물이 있으면 방향 제거
                {
                    availableDirections.Remove(direction);
                }
            }
        }

        // 가능한 방향 중 랜덤으로 선택
        if (availableDirections.Count > 0)
        {
            isConfined = false;
            moveDirection = availableDirections[Random.Range(0, availableDirections.Count)];
        }
        else
        {
            Debug.Log("가능한 방향 없음");
            isConfined = true;
        }
    }

    public void PlayMoveAnimation()
    {
        if(moveDirection == Vector2.up)
        {
            animator.Play("Move_Up");
        }
        else if(moveDirection == Vector2.down)
        {
            animator.Play("Move_Down");
        }
        else if(moveDirection == Vector2.right)
        {
            animator.Play("Move");
            spriteRenderer.flipX = false;
        }
        else if(moveDirection == Vector2.left)
        {
            animator.Play("Move");
            spriteRenderer.flipX = true;
        }
    }


    private void OnDrawGizmos()
    {
        // Raycast 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDirection * rayDistance);
    }
}
