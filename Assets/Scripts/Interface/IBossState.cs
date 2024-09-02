public interface IBossState
{
    BossController bossController{get; set;}
    BossStateMachine stateMachine{get; set;}

    void Update();
    void FixedUpdate();
    void OnEnter();
    void OnExit();
}
