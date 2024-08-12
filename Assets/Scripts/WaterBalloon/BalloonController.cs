using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
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

    public delegate void CheckReadyHandler(BalloonController balloon, Vector2 pos, int power);
    public event CheckReadyHandler OnReady;

    public delegate void BalloonHandler(Vector2 position);
    public event BalloonHandler OnBalloonDestroyed; // 물풍선 파괴

    // 생성할 위치와 애니메이션을 저장할 리스트 (<> : 튜플)
    List<(Vector2 position, string animationName)> AnimationList = new List<(Vector2, string)>();
    
    // 물줄기 저장 리스트
    public List<Vector2>[] streamList = new List<Vector2>[4]; 

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
    public void CheckForReady()
    {
        OnReady?.Invoke(this, transform.position, popLength);
    }


    public void InitializeBalloon(PlayerController controller, int length)
    {
        playerController = controller;
        popLength = length;
    }

    public void initializeStreamList() 
    {
        for (int i = 0; i < streamList.Length; i++) 
        {
            streamList[i] = new List<Vector2>(); // 각 인덱스에 새로운 ArrayList 초기화
        }
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
        SetStreamAnimations();

        // 중앙 팝 오브젝트 생성
        CreateWaterStream(transform.position, "Pop_Center");

        // 모든 방향을 검사한 후, 한 번에 오브젝트 생성
        foreach (var obj in AnimationList)
        {
            CreateWaterStream(obj.position, obj.animationName);
        }
    }



    private void CreateWaterStream(Vector2 position, string animationName)
    {
        GameObject waterStream = Instantiate(popPrefab, position, Quaternion.identity);
        Animator animator = waterStream.GetComponent<Animator>();
        animator.Play(animationName);
    }

    private void SetStreamAnimations()
    {
        string[] midAnimNames = { "Pop_Up", "Pop_Right", "Pop_Down", "Pop_Left" };
        string[] edgeAnimNames = { "Pop_Up_Edge", "Pop_Right_Edge", "Pop_Down_Edge", "Pop_Left_Edge" };

        for (int i = 0; i < streamList.Length; i++)
        {
            // if (streamList[i] == null) break;
            for (int j = 0; j < streamList[i].Count; j++)
            {
                // 애니메이션 이름을 결정
                string animationName;
                if (j == popLength - 1) // popLength와 같은 위치 체크
                {
                    animationName = edgeAnimNames[i];
                }
                else
                {
                    animationName = midAnimNames[i];
                }

                // 위치와 애니메이션을 spawnAnimationList에 추가
                AnimationList.Add((streamList[i][j], animationName));
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

}
