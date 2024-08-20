using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("애니메이터")]
    public RuntimeAnimatorController animatorController;

    [Header("이동 속도")]
    public float moveSpeed;

    [Header("최대 속도")]
    public float maxSpeed;
    
    [Header("물풍선 최대 개수")]
    public int maxBalloonNum;

    [Header("물풍선 개수")]
    public int balloonNum;

    [Header("물줄기 최대 세기")]
    public int maxPopLength;

    [Header("물줄기 세기")]
    public int popLength;
}
