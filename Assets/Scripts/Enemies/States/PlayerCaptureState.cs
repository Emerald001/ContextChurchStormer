using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCaptureState : EnemyState {

    GameObject Player;
    GameObject point;
    NavMeshAgent agent;

    public float counter;

    public PlayerCaptureState(StateMachine<EnemyAI> stateMachine, EnemyAI owner, GameObject Player, NavMeshAgent agent, GameObject point) : base(stateMachine, owner) {
        this.Player = Player;
        this.agent = agent;
        this.point = point;
    }

    public override void OnEnter() {
        agent.SetDestination(owner.transform.position);
        owner.transform.LookAt(Player.transform.position);

        point.SetActive(true);
        agent.speed = 5;
    }

    public override void OnExit() {
        point.SetActive(false);

        agent.SetDestination(owner.transform.position);
        agent.speed = 3.5f;

        point.transform.localScale = new Vector3(0.001f, .001f, .001f);
        point.transform.localPosition = new Vector3(0, 2.8f, 0);
        counter = 0;
    }

    public override void OnUpdate() {
        base.OnUpdate();
         
        if (counter > 3)
            agent.SetDestination(Player.transform.position);
        else {
            point.transform.localScale = Vector3.Lerp(point.transform.localScale, new Vector3(.005f, .005f, .005f), Time.deltaTime);
            point.transform.localPosition = Vector3.Lerp(point.transform.localPosition, new Vector3(0, 5.5f, 0), Time.deltaTime);
            counter += Time.deltaTime;
        }
    }
}