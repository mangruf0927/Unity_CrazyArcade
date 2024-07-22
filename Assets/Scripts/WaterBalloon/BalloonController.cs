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

    public GameObject currentWaterBalloon;

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
}