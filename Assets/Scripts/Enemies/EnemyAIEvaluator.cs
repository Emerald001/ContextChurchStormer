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

        Debug.DrawRay(owner.transform.position + new Vector3(0, .7f, 0), dir, Color.red);

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
}