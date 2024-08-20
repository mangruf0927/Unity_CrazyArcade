using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCenter : MonoBehaviour
{
    private PlayerFactory playerFactory;
    [SerializeField]
    private PlayerData[] playerData;
    [SerializeField]
    private PlayerController controller;

    private void Awake() 
    {
        playerFactory = new PlayerFactory(controller, playerData[(int)DataManager.Instance.GetCharacterType()]);
        playerFactory.CreatePlayer();
    }
}
