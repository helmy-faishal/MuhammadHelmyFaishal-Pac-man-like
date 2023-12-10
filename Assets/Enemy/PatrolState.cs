using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    bool isMoving = false;
    Vector3 destination;
    string triggerName = "Patrol";

    public void EnterState(Enemy enemy)
    {
        Debug.Log("Enter Patrol");
        isMoving = false;
        enemy.animator?.SetTrigger(triggerName);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.IsNearPlayer)
        {
            enemy.SwitchState(enemy.ChaseState);
            return;
        }

        if (!isMoving)
        {
            isMoving = true;

            int index = Random.Range(0, enemy.Waypoints.Count);
            destination = enemy.Waypoints[index].position;
            enemy.Agent.destination = destination;
        }
        else
        {
            // Nilai minimal distance dinaikkan karena terkadang enemy stuck
            if (Vector3.Distance(enemy.transform.position, destination) < 1f)
            {
                isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Patrol");
    }
}
