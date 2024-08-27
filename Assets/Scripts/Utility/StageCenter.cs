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
    [SerializeField]        private Animator profileUIAnimator;

    private PlayerFactory playerFactory;
    
    private void Awake() 
    {
        playerFactory = new PlayerFactory(controller, playerData[(int)DataManager.Instance.GetCharacterType()], profileUIAnimator);
        playerFactory.CreatePlayer();
    }

    private void Start()
    {   
        StartCoroutine(gameStateUI.ShowStartMessage());

        enemy.OnClearStage += ClearStage;
        timerUI.OnEndTime += LoseStage;
        controller.OnDead += PlayerDead;
    }

    private void ClearStage()
    {
        StartCoroutine(ShowClearMessage());
    }

    private void LoseStage()
    {
        StartCoroutine(ShowLoseMessage());
    }

    private void PlayerDead()
    {
        foreach(EnemyController enemy in enemy.enemyList)
        {
            enemy.PlayerDead();
        }

        LoseStage();
    }

    private IEnumerator ShowClearMessage()
    {
        yield return new WaitForSeconds(1f);
        controller.StageClear();
        StartCoroutine(gameStateUI.ShowClearMessage());
        yield return new WaitForSeconds(7f);
        LoadScene("02.Stage2");
    }

    private IEnumerator ShowLoseMessage()
    {
        yield return new WaitForSeconds(1f);
        profileUIAnimator.Play("Lose");
        StartCoroutine(gameStateUI.ShowLoseMessage());
        yield return new WaitForSeconds(7f);
        LoadScene("00.WaitingRoom");
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
