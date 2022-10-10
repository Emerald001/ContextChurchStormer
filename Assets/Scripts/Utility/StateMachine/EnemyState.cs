using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State<EnemyAI> {
    public EnemyAI owner;

    public EnemyState(StateMachine<EnemyAI> stateMachine, EnemyAI owner) : base(stateMachine) {
        this.owner = owner;
    }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void OnUpdate() {
        foreach (var transition in transitions) {
            if (transition.condition.Invoke(owner)) {
                stateMachine.ChangeState(transition.toState);
                return;
            }
        }
    }
}