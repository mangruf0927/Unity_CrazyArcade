using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("애니메이터")]
    public Animator animator;

    [Header("물풍선 상태 머신")]
    public BalloonStateMachine stateMachine;

    [Header("물풍선 대기 시간")]
    public float waitTime;

    [Header("물풍선 프리팹")]
    public GameObject balloonPrefab;

    [Header("물풍선 설치 위치")]
    public Vector3 setPosition;

    [Header("물줄기 프리팹")]
    public GameObject popPrefab;

    [HideInInspector]
    public GameObject currentWaterBalloon;

    private int popLength;
    private PlayerController playerController;

    private void Start() 
    {
        setPosition = transform.position;
        Debug.Log("Start : " + setPosition);
    }

    private void Update() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update(); 

        Debug.Log("물풍선 상태" + stateMachine.curState);
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

    public void DestroyWaterBalloon()
    {
        Destroy(gameObject);
        playerController.GetBalloon();
    }

    public void Explode()
    {
        // 중간
        GameObject waterStreamCenter = Instantiate(popPrefab, transform.position, Quaternion.identity);
        Animator centerAnimator = waterStreamCenter.GetComponent<Animator>();
        centerAnimator.Play("Pop_Center");
        StartCoroutine(DestroyAfterAnimation(centerAnimator, waterStreamCenter));

        // 중간 기점으로 4방향
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        string[] midAnimNames = {"Pop_Up", "Pop_Down", "Pop_Left", "Pop_Right"};
        string[] edgeAnimNames = {"Pop_Up_Edge", "Pop_Down_Edge", "Pop_Left_Edge", "Pop_Right_Edge"};

        for(int i = 0; i < directions.Length; i++)
        {
            Vector3 direction = directions[i];
            string midAnimName = midAnimNames[i];
            string edgeAnimName = edgeAnimNames[i];

            for(int j = 1; j <= popLength; j++)
            {
                Vector3 spawnPosition = transform.position + direction * j;
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

                StartCoroutine(ChangeStateAfterDestroy(centerAnimator, waterStreamCenter));
            }
        }
    }

    private IEnumerator ChangeStateAfterDestroy(Animator animator, GameObject gameObject)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        stateMachine.ChangeState(BalloonStateEnums.DESTROY);
    }

    private IEnumerator DestroyAfterAnimation(Animator animator, GameObject gameObject)
    {
        // 애니메이션 길이를 가져와 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // 오브젝트 파괴
        Destroy(gameObject);
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
}