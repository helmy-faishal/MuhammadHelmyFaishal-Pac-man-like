using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody rb;
    Transform cameraTransform;

    [SerializeField] float powerUpDuration;
    Coroutine powerUpCoroutine;

    [SerializeField] int health = 3;

    [SerializeField] Transform respawnPoint;
    [SerializeField] float respawnDelay;
    Renderer render;
    CapsuleCollider capsuleCollider;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    bool isPowerActive = false;

    bool _isRespawn = false;
    public bool IsRespawn
    {
        get { return _isRespawn; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPowerActive = false;

        UIManager.instance?.SetHealth(health);
    }

    void Update()
    {
        if (!IsRespawn)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 xDirection = horizontal * cameraTransform.right;
        Vector3 zDirection = vertical * cameraTransform.forward;

        Vector3 move = xDirection + zDirection;

        rb.velocity = moveSpeed * Time.fixedDeltaTime * move;
    }

    public void PickedPowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUpCoroutine());
    }

    IEnumerator StartPowerUpCoroutine()
    {
        Debug.Log("Start Power Up");
        OnPowerUpStart?.Invoke();
        isPowerActive = true;

        yield return new WaitForSeconds(powerUpDuration);
        
        Debug.Log("Stop Power Up");
        OnPowerUpStop?.Invoke();
        isPowerActive = false;
    }

    public void TakeDamage()
    {
        health -= 1;

        if (health <= 0) 
        {
            health = 0;
            PlayerDead();
        }
        else
        {
            RespawnPlayer();
        }

        UIManager.instance?.SetHealth(health);
    }

    void PlayerDead()
    {
        Destroy(gameObject);
        Debug.Log("You Lose!");
    }

    void RespawnPlayer()
    {
        rb.velocity = Vector3.zero;
        DisplayRespawn(true);
        StartCoroutine(RespawnDelayCoroutine(respawnDelay));
    }

    void DisplayRespawn(bool respawn)
    {
        _isRespawn = respawn;
        UIManager.instance?.SetRespawnActive(respawn);

        render.enabled = !respawn;
        capsuleCollider.enabled = !respawn;
    }

    IEnumerator RespawnDelayCoroutine(float timeLeft)
    {
        UIManager.instance?.SetRespawnText(timeLeft);
        
        yield return new WaitForSeconds(1);

        timeLeft -= 1;

        if (timeLeft > 0)
        {
            StartCoroutine(RespawnDelayCoroutine(timeLeft));
        }
        else
        {
            transform.position = respawnPoint.position;
            DisplayRespawn(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPowerActive) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyDead();
        }
    }
}
