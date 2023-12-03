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

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
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

        yield return new WaitForSeconds(powerUpDuration);
        
        Debug.Log("Stop Power Up");
        OnPowerUpStop?.Invoke();
    }
}
