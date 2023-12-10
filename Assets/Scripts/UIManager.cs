using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
    int currentCoin = 0;
    int maxCoin = 0;

    [SerializeField] TMP_Text healthText;
    int health = 0;

    [SerializeField] TMP_Text respawnText;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetCoinText();
        SetHealthText();
        SetRespawnActive(false);
    }

    void SetCoinText()
    {
        coinText.text = $"Coin: {currentCoin}/{maxCoin}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    public void SetRespawnActive(bool active)
    {
        respawnText.gameObject.SetActive(active);
    }

    public void SetRespawnText(float timeLeft)
    {
        respawnText.text = "Respawn in " + timeLeft.ToString("0");
    }

    public void SetMaxCoin(int coin)
    {
        maxCoin = coin;
        SetCoinText();
    }

    public void AddCoin(int value)
    {
        currentCoin += value;
        SetCoinText() ;
    }

    public void SetHealth(int value)
    {
        health = value;
        SetHealthText();
    }
}
