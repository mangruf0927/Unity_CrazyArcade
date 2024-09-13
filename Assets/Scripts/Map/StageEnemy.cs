using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : MonoBehaviour
{
    public List<EnemyController> enemyList;
    public bool isBossMap = false;

    public delegate void EnemyHandler();
    public EnemyHandler OnClearStage;
    public EnemyHandler OnEnemyDeath;

    private bool hasTriggerDeath = false;

    public void RemoveEnemy(EnemyController enemy)
    {
        enemyList.Remove(enemy);

        if(!hasTriggerDeath && isBossMap)
        {
            hasTriggerDeath = true;
            OnEnemyDeath?.Invoke();
        }

        CheckEnemyNums(); // 적이 제거될 때마다 체크
    }

    // Stage1 & Stage2 에서만 Clear 
    private void CheckEnemyNums()
    {
        if (enemyList.Count == 0 && !isBossMap)
        {
            OnClearStage?.Invoke();
        }
    }
}
