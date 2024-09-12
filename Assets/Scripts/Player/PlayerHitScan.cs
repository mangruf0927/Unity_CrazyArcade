using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitScan : MonoBehaviour
{
    [Header("충돌 콜라이더")]
    public Collider2D playerCollider;

    [Header("겹치는 면적")]
    [Range(0,1)]
    public float overlapPercent;

    // 플레이어 충돌 체크
    public delegate void HitScanHandler();
    public event HitScanHandler OnTrapPlayer;
    public event HitScanHandler OnTouchEnemy;

    private List<Collider2D> popColliderList = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            popColliderList.Add(other);
            CheckTotalOverlap();
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") 
        || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            OnTouchEnemy?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            popColliderList.Remove(other);
            CheckTotalOverlap();
        }
    }

    private void CheckTotalOverlap()
    {
        float totalOverlapArea = 0f;
        float playerArea = playerCollider.bounds.size.x * playerCollider.bounds.size.y;

        foreach (Collider2D popCollider in popColliderList)
        {
            totalOverlapArea += CalculateOverlapArea(playerCollider, popCollider);
        }

        if (totalOverlapArea >= overlapPercent * playerArea) // 66% 이상 겹치는 경우
        {
            // Debug.Log(totalOverlapArea / playerArea + " % 충돌 !");
            OnTrapPlayer?.Invoke();
        }
    }

    public float CalculateOverlapArea(Collider2D player, Collider2D balloon)
    {
        // 플레이어와 물풍선의 겹치는 영역을 계산 Bounds : 경계영역
        Bounds playerBounds = player.bounds;
        Bounds balloonBounds = balloon.bounds;

        float overlapWidth = Mathf.Max(0, Mathf.Min(playerBounds.max.x, balloonBounds.max.x) - Mathf.Max(playerBounds.min.x, balloonBounds.min.x));
        float overlapHeight = Mathf.Max(0, Mathf.Min(playerBounds.max.y, balloonBounds.max.y) - Mathf.Max(playerBounds.min.y, balloonBounds.min.y));

        return overlapWidth * overlapHeight; // 겹치는 면적 반환
    }
}


