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

    public bool isBoss = true;

    private int popLength;
    private PlayerController playerController;

    // 주변 물풍선 검사 이벤트
    public delegate void CheckReadyHandler(BalloonController balloon, Vector2 pos, int power);
    public event CheckReadyHandler OnReady;

    // 물풍선 파괴 이벤트
    public delegate void BalloonHandler(Vector2 position);
    public event BalloonHandler OnBalloonDestroyed; 

    // 생성할 위치와 애니메이션을 저장할 리스트 (<> : 튜플)
    List<(Vector2 position, string animationName)> AnimationList = new List<(Vector2, string)>();
    
    // 물줄기 저장 리스트
    public List<Vector2>[] streamList = new List<Vector2>[4]; 

    private void Start() 
    {
        initializeStreamList();    
    }

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
    public void InitializerBalloon(GameObject balloon, int length, bool isBoss, PlayerController controller = null)
    {
        balloonPrefab = balloon;
        popLength = length;
        this.isBoss = isBoss;

        playerController = controller;
    }

    public void initializeStreamList() 
    {
        for (int i = 0; i < streamList.Length; i++) 
        {
            streamList[i] = new List<Vector2>(); // 각 인덱스에 새로운 ArrayList 초기화
        }
    }

    public void CheckForReady()
    {
        OnReady?.Invoke(this, transform.position, popLength);
    }

    public void Explode()
    {
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

    public void SetStreamAnimations()
    {
        string[] midAnimNames = { "Pop_Up", "Pop_Right", "Pop_Down", "Pop_Left" };
        string[] edgeAnimNames = { "Pop_Up_Edge", "Pop_Right_Edge", "Pop_Down_Edge", "Pop_Left_Edge" };

        for (int i = 0; i < streamList.Length; i++)
        {
            for (int j = 0; j < streamList[i].Count; j++)
            {
                // 애니메이션 이름을 결정
                string animationName;
                
                if (j == popLength - 1)
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

    private IEnumerator ChangeStateAfterTime(BalloonStateEnums state, float time = 0f)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(state);
    }

    public void StartChangeState(BalloonStateEnums state, float time = 0f)
    {
        StartCoroutine(ChangeStateAfterTime(state, time));
    }

    public void DestroyObject()
    {
        Destroy(currentWaterBalloon);
        currentWaterBalloon = null;
    }

    public void DestroyWaterBalloon()
    {
        OnBalloonDestroyed?.Invoke(transform.position);
        if(playerController != null) playerController.stat.ChargeBalloon();
        
        Destroy(gameObject);
    }
   
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))  
        {
            balloonCollider.isTrigger = false;
        } 
    }

}
