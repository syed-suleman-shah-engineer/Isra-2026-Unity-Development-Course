using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputExample : MonoBehaviour
{
    [Header("Readonly Inputs")]
    public Vector2 moveInput;
    public float shootInput;

    [Header("References")]
    public InputActionAsset inputActions;

    public InputActionReference shootActionRef;


    public void OnEnable()
    {
        inputActions.Enable();


        // inputActions.FindAction("Shoot").performed += OnShootPerformed;
        shootActionRef.action.performed += OnShootPerformed;
    }

    public void OnDisable()
    {
        inputActions.Disable();

        // inputActions.FindAction("Shoot").performed -= OnShootPerformed;
        shootActionRef.action.performed -= OnShootPerformed;
    }

    public void Update()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValue<float>();
    }

}