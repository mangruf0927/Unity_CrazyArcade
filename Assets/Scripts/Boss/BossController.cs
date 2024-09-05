using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("보스 상태머신")]
    public BossStateMachine stateMachine;

    [Header("보스 애니메이터")]
    public Animator animator;

    [Header("리지드 바디")]
    public Rigidbody2D rigid;

    [Header("보스 물풍선")]
    public GameObject balloonPrefab;

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

    // >>
    private bool isBalloonActive = false; // 풍선 활성화 여부 체크
    private Vector2[] spawnPositions = new Vector2[] { new Vector2(0f, -5f), new Vector2(14f, -5f) }; // 생성 위치 배열

    public void StartSetBossBalloon()
    {
        if (!isBalloonActive) // 풍선이 활성화되어 있지 않을 때만 실행
        {
            StartCoroutine(SetBossBalloon());
        }
    }

    private IEnumerator SetBossBalloon()
    {
        isBalloonActive = true; // 풍선 활성화

        // 두 위치에서 번갈아 가며 풍선 생성
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Instantiate(balloonPrefab, spawnPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }

        isBalloonActive = false; // 풍선 비활성화
    }

}
