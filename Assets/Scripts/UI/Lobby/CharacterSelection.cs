using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [Header("배찌 선택 이미지")]
    public GameObject[] bazziSelectedImageArray;

    [Header("다오 선택 이미지")]
    public GameObject[] daoSelectedImageArray;

    [Header("마리드 선택 이미지")]
    public GameObject[] maridSelectedImageArray;

    [Header("랜덤 선택 이미지")]
    public GameObject[] randomSelectedImageArray;

    public Toggle bazziToggle;
    public Toggle daoToggle;
    public Toggle maridToggle;
    public Toggle randomToggle;

    private static readonly int enumLength = System.Enum.GetValues(typeof(CharacterTypeEnums)).Length;

    private void Awake() // 배찌가 디폴트 값
    {
        bazziToggle.isOn = true;
        OnBazziChanged(true);
    }

    private void Start()
    {
        bazziToggle.onValueChanged.AddListener(OnBazziChanged);
        daoToggle.onValueChanged.AddListener(OnDaoChanged);
        maridToggle.onValueChanged.AddListener(OnMaridChanged);
        randomToggle.onValueChanged.AddListener(OnRandomChanged);
    }

    private void OnBazziChanged(bool isOn)
    {
        foreach(GameObject bazzi in bazziSelectedImageArray)
        {
            bazzi.SetActive(isOn);
        }

        if(isOn) DataManager.Instance.SetCharacterType(CharacterTypeEnums.BAZZI);
    }

    private void OnDaoChanged(bool isOn)
    {
        foreach(GameObject dao in daoSelectedImageArray)
        {
            dao.SetActive(isOn);
        }

        if(isOn) DataManager.Instance.SetCharacterType(CharacterTypeEnums.DAO);
    }

    private void OnMaridChanged(bool isOn)
    {
        foreach(GameObject marid in maridSelectedImageArray)
        {
            marid.SetActive(isOn);
        }

        if(isOn) DataManager.Instance.SetCharacterType(CharacterTypeEnums.MARID);
    }

    private void OnRandomChanged(bool isOn)
    {
        foreach(GameObject random in randomSelectedImageArray)
        {
            random.SetActive(isOn);
        }

        if(isOn) DataManager.Instance.SetCharacterType(RandomizeCharacterType());
    }

    public CharacterTypeEnums RandomizeCharacterType()
    {
        CharacterTypeEnums randomType = (CharacterTypeEnums)Random.Range(0, enumLength);
        return randomType;
    }

}
