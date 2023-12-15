using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] Player player;
    List<Pickables> pickables = new List<Pickables>();
    int pickableCoins = 0;

    private void Start()
    {
        Pickables[] pickableObjects = FindObjectsOfType<Pickables>();

        foreach (Pickables pickable in pickableObjects)
        {
            pickables.Add(pickable);
            pickable.onPicked += OnPickablePicked;

            if (pickable.pickType == Pickables.PickType.Coin)
            {
                pickableCoins++;
            }

            UIManager.instance?.SetMaxCoin(pickableCoins);
            
        }
    }

    void OnPickablePicked(Pickables pickable)
    {
        pickables.Remove(pickable);
        //Debug.Log($"Player picked {pickable.pickType}");

        if (pickable.pickType == Pickables.PickType.Power)
        {
            player.PickedPowerUp();
        }

        if (pickable.pickType == Pickables.PickType.Coin)
        {
            pickableCoins--;
            UIManager.instance?.AddCoin(1);
        }

        if (pickableCoins <= 0)
        {
            SceneSwitching.instance?.LoadWinScene();
            Debug.Log("You Win !!!");
        }

        Destroy(pickable.gameObject);
    }
}
