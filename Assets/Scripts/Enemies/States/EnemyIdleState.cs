using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState {

    public bool IsDone;
    public float waitTime;
    public float currentTime;

    public Transform[] positions;
    public int index;

    public EnemyIdleState(StateMachine<EnemyAI> stateMachine, EnemyAI owner, float WaitTime, Transform[] positions) : base(stateMachine, owner) {
        waitTime = WaitTime;
        this.positions = positions;
    }

    public override void OnEnter() {
        IsDone = false;

        currentTime = waitTime;

        index++;
        if (index >= positions.Length)
            index = 0;
    }

    public override void OnExit() {
        IsDone = false;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        owner.transform.eulerAngles = Vector3.MoveTowards(owner.transform.eulerAngles, positions[index].eulerAngles, owner.rotSpeed * Time.deltaTime);

        if (WaitTimer(ref currentTime) < 0)
            IsDone = true;
    }

    public float WaitTimer(ref float Timer) {
        return Timer -= Time.deltaTime;
    }
}