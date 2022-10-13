using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIEvaluator
{
    public EnemyAI owner;

    public EnemyAIEvaluator(EnemyAI owner) {
        this.owner = owner;
    }

    public bool PlayerSeen(GameObject player) {
        Vector3 dir = (player.transform.position - owner.transform.position).normalized;

        float dotProduct = Vector3.Dot(dir, owner.transform.forward);
        if (-dotProduct < Mathf.Cos(owner.viewAngle)) {
            if (Physics.Raycast(owner.transform.position + new Vector3(0, 1, 0), dir, out var hit, owner.viewDis)) {
                if (hit.collider.CompareTag("Player")) {
                    return true;
                }
            }
        }

        return false;
    }

    public bool PlayerBehind(GameObject player) {
        return Vector3.Distance(owner.transform.position, player.transform.position) < 1f;
    }

    public bool GotPlayer(GameObject player) {
        return Vector3.Distance(owner.transform.position, player.transform.position) < .5f;
    }
}