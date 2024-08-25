using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCenter : MonoBehaviour
{
    [SerializeField]        private PlayerData[] playerData;
    [SerializeField]        private PlayerController controller;
    [SerializeField]        private StageEnemy enemy;
    [SerializeField]        private ShowMessage gameStateUI;
    [SerializeField]        private Timer timerUI;


    private PlayerFactory playerFactory;
    
    private void Awake() 
    {
        playerFactory = new PlayerFactory(controller, playerData[(int)DataManager.Instance.GetCharacterType()]);
        playerFactory.CreatePlayer();
    }

    private void Start()
    {   
        StartCoroutine(gameStateUI.ShowStartMessage());

        enemy.OnClearStage += ClearStage;
        timerUI.OnEndTime += LoseStage;
        controller.OnDead += LoseStage;
    }

    private void ClearStage()
    {
        StartCoroutine(ShowClearMessage());
    }

    private void LoseStage()
    {
        StartCoroutine(ShowLoseMessage());
    }

    private IEnumerator ShowClearMessage()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(gameStateUI.ShowClearMessage());
        yield return new WaitForSeconds(7f);
        Debug.Log("클리어");
    }

    private IEnumerator ShowLoseMessage()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(gameStateUI.ShowLoseMessage());
        yield return new WaitForSeconds(7f);
        Debug.Log("루즈");
    }

    private void LoadWaitingRoom(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
