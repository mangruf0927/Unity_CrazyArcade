using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    public delegate bool ASD(Vector2 pos);
    public event ASD OnASD;

    void a()
    {
        bool ispossible = OnASD.Invoke(new Vector2(transform.position.x, transform.position.y));
    }

    void Center()
    {
        OnASD += Check;
    }

    bool Check(Vector2 a)
    {
        return true; // 실제 좌표맵이 설치 가능한지 ? 플레이어에게 돌려줌
    }
}
