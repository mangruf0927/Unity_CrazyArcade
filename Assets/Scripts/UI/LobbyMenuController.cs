using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenuController : MonoBehaviour
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

    CharacterTypeEnums type;
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
        randomToggle.onValueChanged.AddListener(OnRandomChanged);
    }

    private void OnBazziChanged(bool isOn)
    {
        foreach(GameObject bazzi in bazziSelectedImageArray)
        {
            bazzi.SetActive(isOn);
        }

        if(isOn) type = CharacterTypeEnums.BAZZI;
    }

    private void OnDaoChanged(bool isOn)
    {
        foreach(GameObject dao in daoSelectedImageArray)
        {
            dao.SetActive(isOn);
        }

        if(isOn) type = CharacterTypeEnums.DAO;
    }

    private void OnRandomChanged(bool isOn)
    {
        foreach(GameObject random in randomSelectedImageArray)
        {
            random.SetActive(isOn);
        }

        if(isOn) RandomizeCharacterType();
    }

    public void RandomizeCharacterType()
    {
        CharacterTypeEnums randomType = (CharacterTypeEnums)Random.Range(0, enumLength);
        type = randomType;
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("01.Stage1");
        DataManager.Instance.SetCharacterType(type);
    }
}
