using System.Collections;
using System.Collections.Generic;
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

    // : 플레이어 스탯
    [Header("이동 속도")]
    public float speed;
    
    [Header("물풍선 최대 개수")]
    public int maxBalloonNum;
    private int curBalloonNum;

    [Header("물줄기 세기")]
    public int maxPopLength;
    private int curPopLength;

    private bool isTrap = false;

    private void Start() 
    {
        curBalloonNum = 3;
        curPopLength = 2;    
    }

    private void Update()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update();

        Debug.Log("플레이어 상태" + stateMachine.curState);
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }


    // >> :
    public void Move()
    {
        Vector2 moveVector = isHorizontal ? new Vector2(moveDirection.x, 0) : new Vector2(0, moveDirection.y);
        rigid.velocity = moveVector * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
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
        if(curBalloonNum > 0)
        {
            Vector2 setPosition = new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y - 0.25f));
            if(CheckForBalloon(setPosition)) 
            {
                return; // 해당 위치에 다른 오브젝트가 있으면 Return
            }
            else
            {
                curBalloonNum -= 1;
                GameObject waterBalloon = Instantiate(waterBalloonPrefab, setPosition, Quaternion.identity);
                BalloonController balloonController = waterBalloon.GetComponent<BalloonController>();
                balloonController.InitializeBalloon(this, curPopLength);
            }
        }
    }

    public bool CheckForBalloon(Vector2 pos)
    {
        LayerMask layer = LayerMask.GetMask("WaterBalloon");

        // 체크할 위치에서 반경 내에 다른 오브젝트가 있는지 확인
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, 0.3f, layer);

        if (hitColliders.Length > 0) // 해당 위치에 오브젝트 있음
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetBalloon()
    {
        if (curBalloonNum < maxBalloonNum)
        {
            curBalloonNum++;
        }
    }

    public bool CheckTrap()
    {
        return isTrap;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        int popLayer = LayerMask.NameToLayer("Pop");
        if(other.gameObject.layer == popLayer)
        {
            Debug.Log("부딪혔당");
            isTrap = true;
        }
    }
    
    public void StartChangeState(Animator animator)
    {
        StartCoroutine(PlayAnimation(animator));
    }

    private IEnumerator PlayAnimation(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        stateMachine.ChangeLogicState(PlayerStateEnums.DEAD);
    }
}
