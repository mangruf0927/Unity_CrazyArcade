using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // : 플레이어 스탯
    [HideInInspector]
    public float moveSpeed;

    [HideInInspector]
    public float maxSpeed;
    
    [HideInInspector]
    public int maxBalloonNum;

    [HideInInspector]
    public int balloonNum;
    private int curBalloonNum;

    [HideInInspector]
    public int maxPopLength;

    [HideInInspector]
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
