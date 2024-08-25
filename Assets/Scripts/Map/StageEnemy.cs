using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : MonoBehaviour
{
    public List<EnemyController> enemyList;

    public delegate void OnEnemyHandler();
    public OnEnemyHandler OnClearStage;

    public void RemoveEnemy(EnemyController enemy)
    {
        enemyList.Remove(enemy);
        CheckEnemyNums(); // 적이 제거될 때마다 체크
    }

    private void CheckEnemyNums()
    {
        if (enemyList.Count == 0)
        {
            OnClearStage?.Invoke();
        }
    }
}
