using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private CharacterTypeEnums characterType;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetCharacterType(CharacterTypeEnums type)
    {
        characterType = type;
        Debug.Log(characterType);
    }

    public CharacterTypeEnums GetCharacterType()
    {
        return characterType;
    }
}
