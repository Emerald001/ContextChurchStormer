using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] positions;
    public GameObject player;
    public GameObject exclamationPoint;
    public GameObject takeDown;
    public Animator animator;
    public float speed;
    public float rotSpeed;
    public float walkingRange;
    public float viewDis;
    public float viewAngle;
    public float waitTime;

    private NavMeshAgent agent;
    private StateMachine<EnemyAI> enemyStateMachine;
    private EnemyAIEvaluator evaluator;

    private bool isAlive = true;

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        enemyStateMachine = new StateMachine<EnemyAI>(this);
        evaluator = new EnemyAIEvaluator(this);

        var IdleState = new EnemyIdleState(enemyStateMachine, this, waitTime, positions);
        enemyStateMachine.AddState(typeof(EnemyIdleState), IdleState);
        AddTransitionWithPrediquete(IdleState, (x) => { return evaluator.PlayerSeen(player); } , typeof(PlayerCaptureState));
        AddTransitionWithPrediquete(IdleState, (x) => { return IdleState.IsDone; }, typeof(PatrolState));

        var PatrolState = new PatrolState(enemyStateMachine, this, agent, positions);
        enemyStateMachine.AddState(typeof(PatrolState), PatrolState);
        AddTransitionWithPrediquete(PatrolState, (x) => { return evaluator.PlayerSeen(player); }, typeof(PlayerCaptureState));
        AddTransitionWithPrediquete(PatrolState, (x) => { return PatrolState.IsDone; }, typeof(EnemyIdleState));

        var PlayerCaptureState = new PlayerCaptureState(enemyStateMachine, this, player, agent, exclamationPoint);
        enemyStateMachine.AddState(typeof(PlayerCaptureState), PlayerCaptureState);
        AddTransitionWithPrediquete(PlayerCaptureState, (x) => { return !evaluator.PlayerSeen(player); }, typeof(EnemyIdleState));

        enemyStateMachine.ChangeState(typeof(PatrolState));
    }

    void Update() {
        if (!isAlive)
            return;

        enemyStateMachine.OnUpdate();

        if (evaluator.PlayerBehind(player)) {
            takeDown.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                animator.SetTrigger("Dies");
                agent.SetDestination(transform.position);
                isAlive = false;
                takeDown.SetActive(false);
            }
        }
        else {
            takeDown.SetActive(false);
        }

        if (evaluator.GotPlayer(player)) {
            animator.SetBool("TakeDown", true);
            Debug.Log("Lost");
        }
    }

    public void AddTransitionWithKey(State<EnemyAI> state, KeyCode keyCode, System.Type stateTo) {
        state.AddTransition(new Transition<EnemyAI>(
            (x) => {
                if (Input.GetKeyDown(keyCode)) {
                    return true;
                }
                return false;
            }, stateTo));
    }
    public void AddTransitionWithBool(State<EnemyAI> state, bool check, System.Type stateTo) {
        state.AddTransition(new Transition<EnemyAI>(
            (x) => {
                if (check)
                    return true;
                return false;
            }, stateTo));
    }
    public void AddTransitionWithPrediquete(State<EnemyAI> state, System.Predicate<EnemyAI> predicate, System.Type stateTo) {
        state.AddTransition(new Transition<EnemyAI>(predicate, stateTo));
    }
}