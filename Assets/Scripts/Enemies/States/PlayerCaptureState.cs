using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerCaptureState : EnemyState {

    GameObject Player;
    TMP_Text point;
    GameObject pointObject;
    NavMeshAgent agent;

    public float counter;

    public PlayerCaptureState(StateMachine<EnemyAI> stateMachine, EnemyAI owner, GameObject Player, NavMeshAgent agent, GameObject point) : base(stateMachine, owner) {
        this.Player = Player;
        this.agent = agent;
        this.pointObject = point;
        this.point = point.GetComponent<TMP_Text>();
    }

    public override void OnEnter() {
        agent.SetDestination(owner.transform.position);
        owner.transform.LookAt(Player.transform.position);

        pointObject.SetActive(true);
        agent.speed = 5;

        owner.animator.SetBool("Looking", true);
    }

    public override void OnExit() {
        pointObject.SetActive(false);

        agent.SetDestination(owner.transform.position);
        agent.speed = 3.5f;

        point.fontSize = 400;
        counter = 0;

        agent.speed = 5;

        owner.animator.SetBool("Looking", false);
        owner.animator.SetBool("Sprinting", false);
    }

    public override void OnUpdate() {
        base.OnUpdate();
         
        if (counter > 2) {
            owner.animator.SetBool("Sprinting", true);
            agent.SetDestination(Player.transform.position);
            agent.speed = 15;
        }
        else {
            point.fontSize = Mathf.Lerp(point.fontSize, 1000, Time.deltaTime);
            counter += Time.deltaTime;
        }
    }
}