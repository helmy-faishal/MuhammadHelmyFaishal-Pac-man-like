using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Enter Chase");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null)
        {
            enemy.Agent.destination = enemy.player.transform.position;

            if (!enemy.IsNearPlayer)
            {
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Chase");
    }
}
