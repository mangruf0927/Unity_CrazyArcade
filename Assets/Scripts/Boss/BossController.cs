using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("보스 상태머신")]
    public BossStateMachine stateMachine;

    [Header("보스 애니메이터")]
    public Animator animator;

    [Header("리지드 바디")]
    public Rigidbody2D rigid;

    [Header("물풍선")]
    public GameObject waterBalloonPrefab;

    [Header("보스 물풍선")]
    public GameObject bossBalloonPrefab;

    [Header("물폭탄")]
    public GameObject popPrefab;

    [Header("이동 방향")]
    public Vector2 moveDirection;

    [Header("웨이 포인트")]
    public Transform[] wayPointArray;
    private int currentWaypoint;

    [Header("이동 속도")]
    public float moveSpeed;

    // balloonController 등록 
    public delegate void BalloonControllerHandler(Vector2 pos, BalloonController balloon);
    public event BalloonControllerHandler OnControllerReceived;

    // 물풍선 설치 이벤트
    public delegate void BalloonPositionHandler(ObjectTypeEnums type, Vector2 position);
    public event BalloonPositionHandler OnSetBalloon; 

    public delegate bool BossHandler(Vector2 pos);
    public event BossHandler OnCheckShotPosition;

    private Vector2[] spawnPositions = new Vector2[] { new Vector2(0f, -5f), new Vector2(14f, -5f) }; // 생성 위치 배열
    private int spawnIndex = 0;
    public int idleAttackPopLength = 14;
    public int attackPopLength = 3;

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

    // >> IdleAttack
    public void IdleAttack()
    {
        // 좌/우 번갈아가며 풍선 생성
        Vector2 spawnPosition = spawnPositions[spawnIndex];
        spawnIndex = (spawnIndex + 1) % spawnPositions.Length; // 인덱스를 0과 1 사이에서 번갈아가며 선택

        // 물폭탄 공격
        StartCoroutine(IdlePopAttack(spawnPosition, spawnIndex));
    }

    private IEnumerator IdlePopAttack(Vector2 startPosition, int index)
    {
        // 방향 관련 변수 설정
        Vector2[] directions = { Vector2.left, Vector2.right };
        string[] popEdgeAnimation = {"Pop_Left_Edge", "Pop_Right_Edge"};
        string[] popAnimation = { "Pop_Left", "Pop_Right"};
        
        Vector2 direction = directions[index]; // 좌우 방향 설정

        // 반복문을 통해 풍선 생성 및 애니메이션 적용
        for (int i = 0; i < idleAttackPopLength; i++)
        {
            Vector2 spawnPosition = startPosition + direction * i;
            SetBalloonAnimation(i, spawnPosition, popEdgeAnimation[index], popAnimation[index]);
            yield return new WaitForSeconds(0.01f); 
        }
    }

    private void SetBalloonAnimation(int index, Vector2 spawnPosition, string popEdgeAnimation, string popAnimation)
    {
        GameObject waterStream = Instantiate(popPrefab, spawnPosition, Quaternion.identity);
        Animator animator = waterStream.GetComponent<Animator>();

        // 애니메이션 설정
        if (index == 0) // 첫 번째 풍선은 Center 애니메이션
        {
            animator.Play("Pop_Center");
        }
        else if (index == idleAttackPopLength - 1) // 마지막 풍선은 방향에 따른 Edge 애니메이션
        {
            animator.Play(popEdgeAnimation);
        }
        else // 나머지는 방향에 따른 Pop 애니메이션
        {
            animator.Play(popAnimation);
        }
    }

    // >> Move
    public void Move()
    {
        // 현재 웨이포인트로 이동
        if (wayPointArray.Length > 0)
        {
            Transform targetWaypoint = wayPointArray[currentWaypoint];
            moveDirection = (targetWaypoint.position - transform.position).normalized;
            rigid.velocity = moveDirection * moveSpeed;

            // 웨이포인트에 도달하면 다음 웨이포인트로 이동
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % wayPointArray.Length;
            }
        }
    }

    public void PlayMoveAnimation()
    {
        float angle = 15f;

        if (Vector2.Angle(moveDirection, Vector2.up) < angle)
        {
            moveDirection = Vector2.up; // 정확한 방향 설정
            animator.Play("Move_Up");
        }
        else if (Vector2.Angle(moveDirection, Vector2.down) < angle)
        {
            moveDirection = Vector2.down;
            animator.Play("Move_Down");
        }
        else if (Vector2.Angle(moveDirection, Vector2.right) < angle)
        {
            moveDirection = Vector2.right;
            animator.Play("Move_Right");
        }
        else if (Vector2.Angle(moveDirection, Vector2.left) < angle)
        {
            moveDirection = Vector2.left;
            animator.Play("Move_Left");
        }
    }

    // >> Attack
    public void Attack()
    {
        Vector2 startPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)) + moveDirection * 3;
        StartCoroutine(ShootBalloon(startPosition, 3));
    }

    private IEnumerator ShootBalloon(Vector2 startPos, int num)
    {
        Vector2 endPos = GetEndPosition(startPos);

        for(int i = 0; i < num; i++)
        {
            if(!CanShootBalloon(startPos, endPos)) yield break;
            
            GameObject waterBalloon = Instantiate(waterBalloonPrefab, startPos, Quaternion.identity);
            BalloonController balloonController = waterBalloon.GetComponent<BalloonController>();
            balloonController.InitializerBalloon(bossBalloonPrefab, attackPopLength, true);
            
            yield return StartCoroutine(moveBalloon(waterBalloon, startPos, endPos));

            OnSetBalloon?.Invoke(ObjectTypeEnums.BALLOON, endPos);
            OnControllerReceived?.Invoke(endPos, balloonController);
            
            endPos = GetEndPosition(startPos, endPos);
        }
    }

    private bool CanShootBalloon(Vector2 startPos, Vector2 endPos)
    {
        return Vector2.Distance(transform.position, endPos) > Vector2.Distance(transform.position, startPos);
    }

    private Vector2 InitialEndPosition(Vector2 startPos)
    {
        Vector2 endPos = startPos;

        if (moveDirection == Vector2.right)
        {
            endPos.x = 15;
        }
        else if (moveDirection == Vector2.left)
        {
            endPos.x = 0;
        }
        else if (moveDirection == Vector2.up)
        {
            endPos.y = 0;
        }
        else if (moveDirection == Vector2.down)
        {
            endPos.y = -12;
        }
        return endPos;
    }

    private Vector2 GetEndPosition(Vector2 startPos, Vector2? previousPos = null)
    {
        Vector2 endPos = previousPos - moveDirection ?? InitialEndPosition(startPos);

        for (Vector2 curPos = startPos; curPos != endPos;)
        {
            if (OnCheckShotPosition != null && !OnCheckShotPosition(curPos))
            {
                return curPos - moveDirection;
            }

            curPos += moveDirection;
        }
        return endPos;
    }

    private IEnumerator moveBalloon(GameObject balloon, Vector2 startPos, Vector2 endPos)
    {
        float elapsedTime = 0f;
        float moveDuration = 0.3f;

        while (elapsedTime < moveDuration)
        {
            balloon.transform.position = Vector2.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        balloon.transform.position = endPos;
    }
}
