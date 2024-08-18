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

    [Header("플레이어 탐지 범위")]
    public float sensingRange;

    private bool isConfined = false;

    public delegate void EnemyHandler(ObjectTypeEnums type, Vector2 pos);
    public event EnemyHandler OnUpdatePosition;

    public delegate void EnemyRemovePosHandler(Vector2 pos);
    public event EnemyRemovePosHandler OnRemovePosition;

    public delegate bool CheckObstacleHandler(Vector2 pos);
    public event CheckObstacleHandler OnCheckObstacle;

    private Vector2 currentPosition;
    private Vector2 previousPosition;

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
        UpdatePosition();
        CheckForObstacle();

        if(!isConfined)
            rigid.velocity = moveDirection * enemySpeed;
        else
            rigid.velocity = Vector2.zero;
    }

    public void UpdatePosition()
    {
        // 현재 위치를 가져와서 반올림
        Vector2 position = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y));

        // 현재 위치와 이전 위치가 다를 경우에만 업데이트
        if (position != currentPosition)
        {
            // 이전 위치가 존재할 경우 해당 위치 삭제 이벤트 호출
            if (previousPosition != null)
            {
                OnRemovePosition?.Invoke(previousPosition);
            }

            // 새로운 위치 등록 이벤트 호출
            currentPosition = position;
            OnUpdatePosition?.Invoke(ObjectTypeEnums.Enemy, currentPosition);

            // 이전 위치를 현재 위치로 업데이트
            previousPosition = currentPosition;
        }
    }

    public void CheckForObstacle()
    {
        // 이동하려는 방향의 좌표를 계산
        Vector2 targetPosition = currentPosition + moveDirection;

        // 장애물 체크 이벤트 호출
        if (OnCheckObstacle.Invoke(targetPosition))
        {
            SetDirection();
        }
    }

    public void SetDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> directionList = new List<Vector2>();

        foreach (Vector2 direction in directions)
        {
            Vector2 checkPosition = currentPosition + direction;

            // 장애물이 없는 방향을 수집
            if (OnCheckObstacle != null && !OnCheckObstacle.Invoke(checkPosition))
            {
                directionList.Add(direction);
            }
        }

        // 가능한 방향 중 랜덤으로 선택
        if (directionList.Count > 0)
        {
            isConfined = false;
            moveDirection = directionList[Random.Range(0, directionList.Count)];
        }
        else
        {
            isConfined = true;
            Debug.Log("가능한 방향 없음");
        }
    }

    public void CheckForPlayer()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            // 현재 이동 중인 방향은 제외
            if (direction == moveDirection) continue;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sensingRange, LayerMask.GetMask("Player"));

            // 플레이어를 감지한 경우
            if (hit.collider != null)
            {
                moveDirection = direction;
                break; // 한 번 감지되면 다른 방향은 검사하지 않음
            }
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            stateMachine.ChangeState(EnemyStateEnums.DEAD);
        }
    }

    public void StartDestroyEnemy()
    {
        StartCoroutine(DestroyEnemy());
    }
    
    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject, 0.1f);
    }
}
