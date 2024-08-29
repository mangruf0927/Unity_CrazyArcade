using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemyType : MonoBehaviour
{
    public EnemyController enemy;

    public virtual void RoamUpdate()
    {

    }

    public virtual void RoamFixedUpdate()
    {

    }

    public virtual void RoamOnEnter()
    {
        enemy.stateMachine.ChangeState(EnemyStateEnums.MOVE);
    }

    public virtual void RoamOnExit()
    {

    }
}
