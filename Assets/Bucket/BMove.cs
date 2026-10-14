using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BMove : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float range = 2f;

    [Header("Input")]
    public InputActionAsset inputAction;

    [SerializeField]
    private Vector2 moveInput;

    public void OnEnable()
    {
        inputAction.Enable();
    }

    public void OnDisable()
    {
        inputAction.Disable();
    }


    public void Update()
    {
        moveInput = inputAction["Move"].ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        transform.position = transform.position + transform.right * moveInput.x * moveSpeed * Time.fixedDeltaTime;

        float xLimit = Math.Clamp(transform.position.x, -range, range);
        transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
    }
}
