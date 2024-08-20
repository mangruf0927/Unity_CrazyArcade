using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("리지드 바디")]
    public Rigidbody2D rigid;

    [Header("애니메이터")]
    public Animator animator;

    [Header("플레이어 상태머신")]
    public PlayerStateMachine stateMachine;

    [Header("이동 방향")]
    public Vector3 moveDirection;
    public bool isHorizontal;

    [Header("물풍선 프리팹")]
    public GameObject waterBalloonPrefab;

    [Header("플레이어 스탯")]
    public PlayerStat stat;

    [Header("플레이어 히트 스캔")]
    public PlayerHitScan hitScan;

    // 물풍선 설치 가능 여부 확인
    public delegate bool BalloonCheckHandler(Vector2 position);
    public event BalloonCheckHandler OnBalloonCheck;

    // 물풍선 설치 이벤트
    public delegate void BalloonPositionHandler(ObjectTypeEnums type, Vector2 position);
    public event BalloonPositionHandler OnBalloonPlaced; 

    // balloonController 등록 
    public delegate void BalloonControllerHandler(Vector2 pos, BalloonController balloon);
    public event BalloonControllerHandler OnControllerReceived;

    private bool isInstallation = false; // 물풍선 설치 여부
    private bool isTrap = false; // 플레이어 갇혔는지 상태 체크

    private void Start() 
    {
        hitScan.OnDamage += Hit;  
        hitScan.OnTouchEnemy += OnDeath; 
    }

    private void Update()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();

        // Debug.Log("플레이어 상태" + stateMachine.curState);
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }


    // >> :

    public void Move(float speed)
    {
        Vector2 moveVector = isHorizontal ? new Vector2(moveDirection.x, 0) : new Vector2(0, moveDirection.y);
        rigid.velocity = moveVector * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
        // Debug.Log(direction);
    }

    public Vector2 GetDirection()
    {
        return moveDirection;
    }

    public void PlayMoveAnimation()
    {
        if (isHorizontal)
        {
            if (moveDirection.x > 0)
            {
                animator.Play("Move_Right");
            }
            else if (moveDirection.x < 0)
            {
                animator.Play("Move_Left");
            }
        }
        else
        {
            if(moveDirection.y > 0)
            {
                animator.Play("Move_Up");
            }
            else if(moveDirection.y < 0)
            {
                animator.Play("Move_Down");
            }
        }
    }

    public void SetWaterBalloon()
    {
        if(stat.GetCurBalloon() < stat.balloonNum)
        {
            Vector2 setPosition = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y - 0.25f));
            isInstallation = OnBalloonCheck?.Invoke(setPosition) ?? false;

            if(isInstallation) 
            {
                return; // 해당 위치에 다른 오브젝트가 있으면 Return
            }
            else
            {
                OnBalloonPlaced?.Invoke(ObjectTypeEnums.BALLOON, setPosition);
                stat.UseBalloon();

                GameObject waterBalloon = Instantiate(waterBalloonPrefab, setPosition, Quaternion.identity);
                BalloonController balloonController = waterBalloon.GetComponent<BalloonController>();
                balloonController.InitializeBalloon(this, stat.popLength);

                OnControllerReceived?.Invoke(setPosition, balloonController);
            }
        }
    }

    public void Hit()
    {
        isTrap = true;
    }

    public bool CheckTrap()
    {
        return isTrap;
    }

    public void OnDeath()
    {
        stateMachine.ChangeState(PlayerStateEnums.DEAD);
    }
    
    public void StartChangeState(PlayerStateEnums state)
    {
        StartCoroutine(PlayAnimation(state));
    }

    private IEnumerator PlayAnimation(PlayerStateEnums state)
    {
        yield return new WaitForSeconds(0.1f); // 애니메이션이 시작될 시간을 확보

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        stateMachine.ChangeLogicState(state);
    }
}
