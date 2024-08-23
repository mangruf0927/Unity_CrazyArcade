using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : MonoBehaviour
{
    public List<EnemyController> enemyList;

    private void Update() 
    {
        if(enemyList.Count == 0)
            Debug.Log("stage clear");    
    }


}
