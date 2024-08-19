using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [Header("배찌 선택 이미지")]
    public GameObject[] bazziSelectedImageArray;

    [Header("다오 선택 이미지")]
    public GameObject[] daoSelectedImageArray;

    public Toggle bazziToggle;
    public Toggle daoToggle;

    private void Awake() 
    {
        bazziToggle.isOn = true;
        OnBazziChanged(true);
    }

    private void Start()
    {
        bazziToggle.onValueChanged.AddListener(OnBazziChanged);
        daoToggle.onValueChanged.AddListener(OnDaoChanged);
    }

    private void OnBazziChanged(bool isOn)
    {
        foreach(GameObject bazzi in bazziSelectedImageArray)
        {
            bazzi.SetActive(isOn);
        }
    }

    private void OnDaoChanged(bool isOn)
    {
        foreach(GameObject dao in daoSelectedImageArray)
        {
            dao.SetActive(isOn);
        }
    }
}
