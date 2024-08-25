using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCenter : MonoBehaviour
{
    [SerializeField]        private PlayerData[] playerData;
    [SerializeField]        private PlayerController controller;
    [SerializeField]        private StageEnemy enemy;
    [SerializeField]        private ShowMessage message;

    private PlayerFactory playerFactory;
    
    private void Awake() 
    {
        playerFactory = new PlayerFactory(controller, playerData[(int)DataManager.Instance.GetCharacterType()]);
        playerFactory.CreatePlayer();
    }

    private void Start()
    {
    }

    private void LoadWaitingRoom(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
