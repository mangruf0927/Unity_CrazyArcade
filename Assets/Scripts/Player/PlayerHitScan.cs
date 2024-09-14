using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitScan : MonoBehaviour
{
    [Header("물풍선 충돌 체크용 콜라이더")]
    public Collider2D trapCollider; 

    [Header("적 충돌 체크용 콜라이더")]
    public Collider2D bodyCollider;  

    [Header("겹치는 면적")]
    [Range(0,1)]
    public float overlapPercent;

    // 플레이어 충돌 체크
    public delegate void HitScanHandler();
    public event HitScanHandler OnTrapPlayer;
    public event HitScanHandler OnTouchEnemy;

    private List<Collider2D> popColliderList = new List<Collider2D>();

    public bool isBossTrap = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 물풍선 충돌 체크용 콜라이더로 감지하는 경우
        if (trapCollider.IsTouching(other) && other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            popColliderList.Add(other);
            CheckTotalOverlap();
        }

        // 적 충돌 체크용 콜라이더로 감지하는 경우
        if (bodyCollider.IsTouching(other) && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnTouchEnemy?.Invoke();  // 적과 충돌 처리
        }

        // 보스 충돌 체크용
        if (!isBossTrap && bodyCollider.IsTouching(other) && other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            OnTouchEnemy?.Invoke();  // 보스와 충돌 처리
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (trapCollider.IsTouching(other) && other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            popColliderList.Remove(other);
        }
    }

    private void CheckTotalOverlap()
    {
        float totalOverlapArea = 0f;
        float playerArea = trapCollider.bounds.size.x * trapCollider.bounds.size.y;

        foreach (Collider2D popCollider in popColliderList)
        {
            totalOverlapArea += CalculateOverlapArea(trapCollider, popCollider);
        }

        if (totalOverlapArea >= overlapPercent * playerArea)
        {
            OnTrapPlayer?.Invoke();  // 플레이어가 트랩에 갇혔을 때 처리
        }
    }

    public float CalculateOverlapArea(Collider2D player, Collider2D balloon)
    {
        Bounds playerBounds = player.bounds;
        Bounds balloonBounds = balloon.bounds;

        float overlapWidth = Mathf.Max(0, Mathf.Min(playerBounds.max.x, balloonBounds.max.x) - Mathf.Max(playerBounds.min.x, balloonBounds.min.x));
        float overlapHeight = Mathf.Max(0, Mathf.Min(playerBounds.max.y, balloonBounds.max.y) - Mathf.Max(playerBounds.min.y, balloonBounds.min.y));

        return overlapWidth * overlapHeight;
    }
}

