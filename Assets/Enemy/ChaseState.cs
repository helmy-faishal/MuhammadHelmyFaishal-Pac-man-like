using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    string triggerName = "Chase";

    public void EnterState(Enemy enemy)
    {
        Debug.Log("Enter Chase");
        enemy.animator?.SetTrigger(triggerName);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player == null) return;

        enemy.Agent.destination = enemy.player.transform.position;

        if (!enemy.IsNearPlayer)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Chase");
    }
}
