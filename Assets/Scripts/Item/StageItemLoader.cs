using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItemLoader : MonoBehaviour
{
    public List<Skate> skateList;
    public List<Balloon> balloonList;
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
