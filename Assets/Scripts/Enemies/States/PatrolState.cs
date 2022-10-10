using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState {

    public bool IsDone = false;

    public NavMeshAgent agent;
    public int index;

    public Transform[] positions;

    public PatrolState(StateMachine<EnemyAI> stateMachine, EnemyAI owner, NavMeshAgent agent, Transform[] positions) : base(stateMachine, owner) {
        this.agent = agent;
        this.positions = positions;
    }

    public override void OnEnter() {
        IsDone = false;

        index++;

        if (index >= positions.Length)
            index = 0;

        agent.SetDestination(positions[index].position);
    }

    public override void OnExit() {
        IsDone = false;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (owner.transform.position == agent.destination)
            IsDone = true;
    }
}