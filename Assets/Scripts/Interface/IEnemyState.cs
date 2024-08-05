public interface IEnemyState
{
    EnemyController enemyController{get; set;}
    EnemyStateMachine stateMachine{get; set;}

    void Update();
    void FixedUpdate();
    void OnEnter();
    void OnExit();
}
