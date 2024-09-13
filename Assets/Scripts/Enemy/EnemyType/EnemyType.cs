using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public EnemyController enemyController;

    public virtual void MoveOnExit()
    {

    }

    public virtual void RoamUpdate()
    {

    }

    public virtual void RoamFixedUpdate()
    {

    }

    public virtual void RoamOnEnter()
    {
        enemyController.stateMachine.ChangeState(EnemyStateEnums.MOVE);
    }

    public virtual void RoamOnExit()
    {

    }
}
