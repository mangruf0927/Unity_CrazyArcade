using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("물풍선 상태 머신")]
    public BalloonStateMachine stateMachine;

    [Header("물풍선 프리팹")]
    public GameObject balloonPrefab;

    [Header("물풍선 설치 위치")]
    public Vector3 setPosition;

    [Header("물줄기 프리팹")]
    public GameObject popPrefab;

    [Header("물풍선 콜라이더")]
    public Collider2D balloonCollider;

    [HideInInspector] 
    public GameObject currentWaterBalloon;

    private int popLength;
    private PlayerController playerController;

    // 물풍선 설치 가능 여부 확인
    public delegate bool StreamCheck(Vector2 position);
    public event StreamCheck OnStreamCheck;

    // 물풍선 파괴 이벤트
    public delegate void BalloonDestroyHandler(Vector2 position);
    public event BalloonDestroyHandler OnBalloonDestroyed;


    private void Update() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update(); 
        
        // Debug.Log("물풍선 상태" + stateMachine.curState);
    }

    private void FixedUpdate()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }

    // : 만든 함수들 
    public void InitializeBalloon(PlayerController controller, int length)
    {
        playerController = controller;
        popLength = length;
    }

    public IEnumerator ChangeStateAfterTime(float time, BalloonStateEnums state)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(state);
    }

    public void StartChangeState(float time, BalloonStateEnums state)
    {
        StartCoroutine(ChangeStateAfterTime(time, state));
    }

    public void DestroyObject()
    {
        Destroy(currentWaterBalloon);
        currentWaterBalloon = null;
    }

    public void DestroyWaterBalloon()
    {
        OnBalloonDestroyed?.Invoke(transform.position);
        Destroy(gameObject);
        playerController.stat.ChargeBalloon();
    }

    public void Explode()
    {
        // 중간 
        GameObject waterStreamCenter = Instantiate(popPrefab, transform.position, Quaternion.identity);
        Animator centerAnimator = waterStreamCenter.GetComponent<Animator>();
        centerAnimator.Play("Pop_Center");
        StartCoroutine(ChangeStateAfterDestroy(centerAnimator, waterStreamCenter));

        // 중간 기점으로 4방향
        Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};
        string[] midAnimNames = {"Pop_Up", "Pop_Down", "Pop_Left", "Pop_Right"};
        string[] edgeAnimNames = {"Pop_Up_Edge", "Pop_Down_Edge", "Pop_Left_Edge", "Pop_Right_Edge"};
        
        for(int i = 0; i < directions.Length; i++)
        {
            Vector2 direction = directions[i];
            string midAnimName = midAnimNames[i];
            string edgeAnimName = edgeAnimNames[i];

            for(int j = 1; j <= popLength; j++)
            {
                // 4방향으로 장애물 있는지 확인
                Debug.DrawRay(transform.position, direction * j, Color.red, 2f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, j, LayerMask.GetMask("Border"));
                if(hit.collider != null) break;

                // 장애물 없으면 물줄기 설치
                Vector3 spawnPosition = (Vector2)transform.position + direction * j;
                GameObject waterStream = Instantiate(popPrefab, spawnPosition, Quaternion.identity);
                Animator animator = waterStream.GetComponent<Animator>();

                if(j == popLength) 
                {
                    animator.Play(edgeAnimName);
                }
                else
                {
                    animator.Play(midAnimName);
                }

                StartCoroutine(DestroyAfterAnimation(animator, waterStream));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject == playerController.gameObject)  
        {
            balloonCollider.isTrigger = false;
        } 
    }

    private IEnumerator DestroyAfterAnimation(Animator animator, GameObject gameObject)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private IEnumerator ChangeStateAfterDestroy(Animator animator, GameObject gameObject)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        stateMachine.ChangeState(BalloonStateEnums.DESTROY);
    }
}
