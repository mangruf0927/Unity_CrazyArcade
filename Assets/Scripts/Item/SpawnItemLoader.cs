using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class SpawnItemLoader : MonoBehaviour
{
    [Header("블록 리스트")]
    public StageBlock block;

    [Header("스폰될 아이템 목록")]
    public GameObject[] spawnItemArray;
    
    [Header("아이템 생성 확률")]
    [Range(0, 100)]
    public int spawnProbability; 

    [Header("아이템 센터")]
    public ItemCenter itemCenter;

    private void Start() 
    {
        block.OnBlockDestruction+=SpawnItem;
    }

    public void SpawnItem(Vector2 position)
    {
        // 아이템 생성 확률 확인
        if (Random.Range(0, 100) < spawnProbability)
        {
            // 랜덤으로 아이템 선택
            int randomIndex = Random.Range(0, spawnItemArray.Length);
            GameObject itemPrefab = spawnItemArray[randomIndex];

            // 아이템 생성
            GameObject spawnedItem = Instantiate(itemPrefab, position, Quaternion.identity);

            // 생성된 아이템 등록
            RegisterItem(spawnedItem);
        }
        else
        {
            Debug.Log("아이템이 생성되지 않았습니다.");
        }
    }

    private void RegisterItem(GameObject item)
    {
        // 아이템의 종류에 따라 등록
        if (item.TryGetComponent<Skate>(out Skate skate))
        {
            itemCenter.RegisterSkate(skate);
        }
        else if (item.TryGetComponent<Balloon>(out Balloon balloon))
        {
            itemCenter.RegisterBalloon(balloon);
        }
        else if (item.TryGetComponent<Potion>(out Potion potion))
        {
            itemCenter.RegisterPotion(potion);
        }

        // TryGetComponent :해당 컴퍼넌트를 찾지 못했을 때 memory allocation이 발생하지 않고 
        // Gabage Collection을 걱정할 필요없다.
    }

}
