using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItemLoader : MonoBehaviour
{
    [Header("스케이트 아이템 목록")]
    public List<Skate> skateList;

    [Header("물풍선 아이템 목록")]    
    public List<Balloon> balloonList;

    [Header("물약 아이템 목록")]
    public List<Potion> potionList;

    public ItemCenter itemCenter;

    private void Start() 
    {
        RegisterItems();    
    }

    private void RegisterItems()
    {
        foreach (Skate skate in skateList)
        {
            // 아이템 센터에 아이템 등록
            itemCenter.RegisterSkate(skate);
        }

        foreach (Balloon balloon in balloonList)
        {
            itemCenter.RegisterBalloon(balloon);
        }

        foreach (Potion potion in potionList)
        {
            itemCenter.RegisterPotion(potion);
        }
    }
}
