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
    [SerializeField]        private BossController boss;
    [SerializeField]        private GameObject HelpUI;
    public BossHP bossHP;

    public int nextSceneNum;
    public int lobbySceneNum;

    private bool isClear = false;

    private PlayerFactory playerFactory;
    
    private void Awake() 
    {
        playerFactory = new PlayerFactory(controller, playerData[(int)DataManager.Instance.GetCharacterType()], profileUIAnimator);
        playerFactory.CreatePlayer();

        if(boss != null) boss.stat.AddObserver<IObserver>(boss.stat.hpObserverList, bossHP);
    }

    private void Start()
    {   
        StartCoroutine(gameStateUI.ShowStartMessage());

        enemy.OnClearStage += ClearStage;
        timerUI.OnEndTime += LoseStage;
        controller.OnPlayerDead += PlayerDead;
        controller.hitScan.OnTrapPlayer += TrapPlayer;
    }

    private void ClearStage()
    {
        isClear = true;
        StartCoroutine(ShowClearMessage());
    }

    private void LoseStage()
    {
        if(!isClear)
        {
            StartCoroutine(ShowLoseMessage());
        }
    }

    private void TrapPlayer()
    {
        HelpUI.SetActive(true);
    }

    private void PlayerDead()
    {
        foreach(EnemyController enemy in enemy.enemyList)
        {
            enemy.isPlayerDead = true;
        }
        HelpUI.SetActive(false);
        LoseStage();
    }

    private IEnumerator ShowClearMessage()
    {
        yield return new WaitForSeconds(1f);
        controller.StageClear();
        StartCoroutine(gameStateUI.ShowClearMessage());
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(nextSceneNum);
    }

    private IEnumerator ShowLoseMessage()
    {
        yield return new WaitForSeconds(1f);
        profileUIAnimator.Play("Lose");
        StartCoroutine(gameStateUI.ShowLoseMessage());
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(lobbySceneNum);
    }
}
