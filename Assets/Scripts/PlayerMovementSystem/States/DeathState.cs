using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : MoveState {
    public DeathState(StateMachine<MovementManager> owner) : base(owner) {
    }

    public override void OnEnter() {
        owner.animator.SetBool("Dies", true);
    }

    public override void OnExit() {

    }

    public override void OnUpdate() {
        base.OnUpdate();
    }
}