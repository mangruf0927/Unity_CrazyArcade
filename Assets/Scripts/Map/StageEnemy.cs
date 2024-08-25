using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : MonoBehaviour
{
    public List<EnemyController> enemyList;

    public delegate void OnEnemyHandler();
    public OnEnemyHandler OnCheckStageEnemy;


    private void Update() 
    {
        if(enemyList.Count == 0)
        {
            OnCheckStageEnemy?.Invoke();
        }
    }


}
