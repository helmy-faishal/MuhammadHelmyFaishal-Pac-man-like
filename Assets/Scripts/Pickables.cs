using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickables : MonoBehaviour
{
    public enum PickType
    {
        Coin,
        Power
    }

    public PickType pickType;
    public Action<Pickables> onPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPicked?.Invoke(this);
        }
    }
}
