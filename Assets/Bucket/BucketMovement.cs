using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BucketMovement : MonoBehaviour
{

    [Header("Inputs")]
    public InputActionAsset inputActionAsset;

    [Header("Settings")]
    public float horizontalMoveSpeed = 5f;
    public float boundary = 10;


    [SerializeField] private Vector2 _moveInput;


    public void Start()
    {
    }


    private void OnEnable()
    {
        inputActionAsset.Enable();
    }

    private void OnDisable()
    {
        inputActionAsset.Disable();
    }

    private void Update()
    {
        _moveInput = inputActionAsset["Move"].ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + transform.right * _moveInput.x * Time.fixedDeltaTime;
        transform.position = new Vector3(Math.Clamp(transform.position.x, -boundary, boundary), transform.position.y, transform.position.z);
    }

}
