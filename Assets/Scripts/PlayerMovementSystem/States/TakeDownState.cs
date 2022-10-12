using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDownState : MoveState
{
    public TakeDownState(StateMachine<MovementManager> owner) : base(owner) {

    }

    public float timer = 0;
    public bool IsDone = false;

    public override void OnEnter() {
        owner.velocity = Vector3.zero;

        owner.animator.SetBool("TakeDown", true); 
        
        IsDone = false;
    }

    public override void OnExit() {
        owner.animator.SetBool("TakeDown", false);

        timer = 1.2f;
        IsDone = false;
    }

    public override void OnUpdate() {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }
        else {
            IsDone = true;
        }

        base.OnUpdate();
    }
}