using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [Header("배찌 이미지")]
    public GameObject[] bazziSpriteArray;

    [Header("다오 이미지")]
    public GameObject[] daoSpriteArray;

    [Header("배찌 토글")]
    public Toggle bazziToggle; 

    [Header("다오 토글")]
    public Toggle daoToggle; 

    private void Awake()
    {
        bazziToggle.onValueChanged.AddListener(bazziSprite);
        daoToggle.onValueChanged.AddListener(daoSprite);
    }

    public void bazziSprite(bool isActive)
    {
        foreach (GameObject bazzi in bazziSpriteArray)
        {
            bazzi.SetActive(isActive);
        }
    }

    public void daoSprite(bool isActive)
    {
        foreach (GameObject dao in daoSpriteArray)
        {
            dao.SetActive(isActive);
        }
    }
}