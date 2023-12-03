using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Enter Retreat");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null)
        {
            enemy.Agent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Retreat");
    }
}
