using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableBox : MonoBehaviour
{
    private Vector2 pushDirection; // 이동 방향
    private bool isMoving = false;  
    private Coroutine pushingCoroutine = null;

    public delegate ObjectTypeEnums MovableBoxBoolHandler(Vector2 pos);
    public event MovableBoxBoolHandler OnMoveCheck;

    public delegate void BoxRegisterHandler(ObjectTypeEnums type, Vector2 pos);
    public event BoxRegisterHandler OnRegisterNewPos;

    public delegate void RemoveBoxHandler(Vector2 pos);
    public event RemoveBoxHandler OnRemoveOriginPos;

    public delegate void ChangePositionHandler(Vector2 oldPos, Vector2 newPos);
    public event ChangePositionHandler OnChangePos;

    public delegate Vector2 GetDirectionHandler();
    public event GetDirectionHandler OnGetDirection;

    void OnCollisionStay2D(Collision2D collision) // 플레이어가 박스를 밀 때
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isMoving)
        {
            if (pushingCoroutine == null)
            {
                pushingCoroutine = StartCoroutine(PushBox(collision));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) // 플레이어가 박스에서 떨어질 때
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (pushingCoroutine != null)
            {
                StopCoroutine(pushingCoroutine);
                pushingCoroutine = null;
            }
        }
    }

    private IEnumerator PushBox(Collision2D collision)
    {
        float elapsedTime = 0f;
        
        Vector2 direction = CalculatePushDirection(collision);
        Vector2 playerDirection;

        // 1초 동안 방향이 일치하는지 체크
        while (elapsedTime < 0.8f)
        {
            playerDirection = OnGetDirection?.Invoke() ?? Vector2.zero;

            if (playerDirection != direction)
            {
                pushingCoroutine = null;
                yield break; // 방향이 다르면 코루틴 종료
            }

            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 방향이 1초동안 같으면 박스 밀기
        TryMove(direction);
        pushingCoroutine = null;
    }

    public void TryMove(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)transform.position + direction;

        if (OnMoveCheck?.Invoke(newPosition) == ObjectTypeEnums.NONE)
        {
            StartCoroutine(MoveBox(newPosition));
        }
    }

    private IEnumerator MoveBox(Vector2 newPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        float moveDuration = 0.25f;
        Vector2 originalPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(originalPosition, newPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = newPosition;
        isMoving = false;

        OnRegisterNewPos?.Invoke(ObjectTypeEnums.BOX, newPosition);
        OnChangePos?.Invoke(originalPosition, newPosition);
        OnRemoveOriginPos?.Invoke(originalPosition);
    }

    private Vector2 CalculatePushDirection(Collision2D collision)
    {
        Vector2 boxPosition = transform.position;
        Vector2 playerPosition = collision.transform.position;
        Vector2 difference = playerPosition - boxPosition;

        if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x)) // 수직 방향 체크
        {
            pushDirection = difference.y > 0 ? Vector2.down : Vector2.up;
        }
        else // 수평 방향 체크
        {
            pushDirection = difference.x > 0 ? Vector2.left : Vector2.right;
        }

        return pushDirection;
    }
}