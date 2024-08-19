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

    [Header("랜덤 선택 이미지")]
    public GameObject[] randomSelectedImageArray;

    public Toggle bazziToggle;
    public Toggle daoToggle;
    public Toggle randomToggle;

    private void Awake() // 배찌가 디폴트 값
    {
        bazziToggle.isOn = true;
        OnBazziChanged(true);
    }

    private void Start()
    {
        bazziToggle.onValueChanged.AddListener(OnBazziChanged);
        daoToggle.onValueChanged.AddListener(OnDaoChanged);
        randomToggle.onValueChanged.AddListener(OnRandomChanged);
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

    private void OnRandomChanged(bool isOn)
    {
        foreach(GameObject random in randomSelectedImageArray)
        {
            random.SetActive(isOn);
        }
    }
}
