using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // : 플레이어 스탯
    [Header("이동 속도")]
    public float moveSpeed;

    [Header("최대 속도")]
    public float maxSpeed;
    
    [Header("물풍선 최대 개수")]
    public int maxBalloonNum;

    [Header("물풍선 개수")]
    public int balloonNum;
    private int curBalloonNum;

    [Header("물줄기 최대 세기")]
    public int maxPopLength;

    [Header("물줄기 세기")]
    public int popLength;

    private void Start() 
    {
        curBalloonNum = 0;    
    }

    // >> :
    public int GetCurBalloon()
    {
        return curBalloonNum;
    }

    public void ChargeBalloon()
    {
        if (curBalloonNum > 0)
        {
            curBalloonNum--;
        }
    }

    public void UseBalloon()
    {
        curBalloonNum++;
    }

    public void AddBalloon()
    {
        if(balloonNum < maxBalloonNum)
        {
            balloonNum ++;
        }
    }

    public void AddSpeed()
    {
        if(moveSpeed < maxSpeed)
        {
            moveSpeed ++;
        }
    }

    public void AddPotion()
    {
        if(popLength < maxPopLength)
        {
            popLength++;
        }
    }

    public void SetBalloonNum(int init, int max)
    {
        balloonNum = init;
        maxBalloonNum = max;
    }

    public void SetSpeed(float init, float max)
    {
        moveSpeed = init;
        maxSpeed = max;
    }

    public void SetPopLength(int init, int max)
    {
        popLength = init;
        maxPopLength = max;
    }
}
