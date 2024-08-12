using System.Collections;
using System.Collections.Generic;
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

    // 물풍선 설치 가능 여부 확인
    public delegate ObjectTypeEnums StreamCheckHandler(Vector2 position);
    public event StreamCheckHandler OnStreamCheck;

    public delegate void BalloonHandler(Vector2 position);
    public event BalloonHandler OnRemoveBox; // box 설치 시 제거
    public event BalloonHandler OnChangeBalloonState; // 4방향 물풍선 찾기
    public event BalloonHandler OnBalloonDestroyed; // 물풍선 파괴

    // 생성할 위치와 애니메이션을 저장할 리스트 (<> : 튜플)
    List<(Vector2 position, string animationName)> spawnList = new List<(Vector2, string)>();


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

    public void CheckFourWays()
    {
        // 4방향 검사 후 생성할 위치 저장 (시계방향으로 검사)
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        string[] midAnimNames = { "Pop_Up", "Pop_Right", "Pop_Down", "Pop_Left" };
        string[] edgeAnimNames = { "Pop_Up_Edge","Pop_Right_Edge", "Pop_Down_Edge", "Pop_Left_Edge"};

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 direction = directions[i];
            string midAnimName = midAnimNames[i];
            string edgeAnimName = edgeAnimNames[i];

            for (int j = 1; j <= popLength; j++)
            {
                Vector2 spawnPosition = (Vector2)transform.position + direction * j;

                // 위치 체크 후 필요한 경우 리스트에 추가
                if (CheckPosition(spawnPosition, ref spawnList, j == popLength ? edgeAnimName : midAnimName)) 
                {
                    break;
                }
            }
        }
    }

    public void Explode()
    {
        // 중앙 팝 오브젝트 생성
        CreateWaterStream(transform.position, "Pop_Center", true);

        // 모든 방향을 검사한 후, 한 번에 오브젝트 생성
        foreach (var obj in spawnList)
        {
            CreateWaterStream(obj.position, obj.animationName, false);
        }
    }

    private bool CheckPosition(Vector2 position, ref List<(Vector2, string)> spawnList, string animationName)
    {
        ObjectTypeEnums type = OnStreamCheck?.Invoke(position) ?? ObjectTypeEnums.None;

        if (type == ObjectTypeEnums.Object) 
        {
            return true;
        }
        else if (type == ObjectTypeEnums.Box)
        {
            OnRemoveBox?.Invoke(position);
            return true;
        }
        else if(type == ObjectTypeEnums.Balloon)
        {
            OnChangeBalloonState?.Invoke(position);
            return false;
        }
        else if (type == ObjectTypeEnums.None)
        {
            // 리스트에 위치와 애니메이션을 저장
            spawnList.Add((position, animationName));
            return false;
        }
        return false;
    }

    private void CreateWaterStream(Vector2 position, string animationName, bool isCenter)
    {
        GameObject waterStream = Instantiate(popPrefab, position, Quaternion.identity);
        Animator animator = waterStream.GetComponent<Animator>();
        animator.Play(animationName);

        if(isCenter) StartCoroutine(ChangeStateAfterDestroy(animator));
    }
   
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject == playerController.gameObject)  
        {
            balloonCollider.isTrigger = false;
        } 
    }

    private IEnumerator ChangeStateAfterDestroy(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        // Destroy(gameObject);
        stateMachine.ChangeState(BalloonStateEnums.DESTROY);
    }
}
