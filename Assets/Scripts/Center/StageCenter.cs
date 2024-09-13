using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageCenter : MonoBehaviour
{
    [Header("플레이어 데이터")]
    [SerializeField]        private PlayerData[] playerData;
    
    [Header("플레이어 컨트롤러")]
    [SerializeField]        private PlayerController playerController;
    
    [Header("보스 컨트롤러")]
    [SerializeField]        private BossController bossController;
    public BossHP bossHP;
    
    [Header("Stage Enemy")]
    [SerializeField]        private StageEnemy enemy;

    [Header("HUD 센터")]
    [SerializeField]        private HUDCenter hudCenter;

    [Header("타이머")]
    [SerializeField]        private Timer timer;

    [Header("씬 전환 번호")]
    public int nextSceneNum;
    public int lobbySceneNum;

    private bool isClear = false;
    private PlayerFactory playerFactory;
    
    private void Awake() 
    {
        playerFactory = new PlayerFactory(playerController, playerData[(int)DataManager.Instance.GetCharacterType()], hudCenter.profileUIAnimator);
        playerFactory.CreatePlayer();

        if(bossController != null) bossController.stat.AddObserver<IObserver>(bossController.stat.hpObserverList, bossHP);
    }

    private void Start()
    {   
        StartCoroutine(hudCenter.ShowStartMessage());

        enemy.OnClearStage += ClearStage;
        enemy.OnEnemyDeath += ChangeBossState; 

        timer.OnEndTime += LoseStage;
        
        playerController.OnPlayerDead += PlayerDead;
        playerController.hitScan.OnTrapPlayer += TrapPlayer;
    }

    private void ClearStage()
    {
        isClear = true;
        StartCoroutine(ShowClearMessage());
        
        playerController.stateMachine.ChangeState(PlayerStateEnums.CLEAR);
    }

    private void ChangeBossState()
    {
        if(bossController.stateMachine.CheckCurState(BossStateEnums.IDLEATTACK))
        {
            bossController.stateMachine.ChangeState(BossStateEnums.MOVE);
        }
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
        hudCenter.PlayerTrapUI(true);
    }

    private void PlayerDead()
    {
        foreach(EnemyController enemy in enemy.enemyList)
        {
            enemy.isPlayerDead = true;
        }
        hudCenter.PlayerTrapUI(false);
        LoseStage();
    }

    public void OnClickExitButton()
    {
        SceneManager.LoadScene(lobbySceneNum);
    }

    private IEnumerator ShowClearMessage()
    {
        yield return new WaitForSeconds(1f);
        playerController.StageClear();
        yield return StartCoroutine(hudCenter.ShowClearMessage());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneNum);
    }

    private IEnumerator ShowLoseMessage()
    {
        yield return new WaitForSeconds(1f);
        hudCenter.PlayProfileAnimation();
        yield return StartCoroutine(hudCenter.ShowLoseMessage());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(lobbySceneNum);
    }
}
