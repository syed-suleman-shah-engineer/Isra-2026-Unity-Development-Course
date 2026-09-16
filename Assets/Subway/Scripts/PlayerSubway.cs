using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerSubway : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("Lane Settings")]
    [Tooltip("Distance from center to each side lane (e.g. 4 means lanes at -4, 0, 4)")]
    public float laneDistance = 4f;
    [Tooltip("How fast the player slides between lanes")]
    public float laneChangeSpeed = 10f;

    [Header("Jump")]
    public float jumpHeight = 2f;
    public float gravity = -20f;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 10f;

    [Header("Input Reference")]
    public InputActionAsset inputActions;

    // Lane state: -1 = left, 0 = center, 1 = right
    private int _currentLane = 0;
    private float _targetX = 0f;

    // Vertical velocity for jump/gravity
    private float _verticalVelocity;

    // Crouch state
    private bool _isCrouching;
    private float _targetHeight;

    // Input flags (edge-triggered, set in event, consumed in FixedUpdate)
    private bool _jumpPressed;
    private bool _crouchPressed;
    private bool _crouchReleased;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        _targetHeight = standHeight;
        controller.height = standHeight;

        // Center the controller so feet are at transform.position
        Vector3 c = controller.center;
        c.y = standHeight / 2f;
        controller.center = c;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        var moveAction = inputActions.FindAction("Move");
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        var moveAction = inputActions.FindAction("Move");
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;

        inputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        // Horizontal lane change
        if (input.x > 0.5f) ChangeLane(1);
        else if (input.x < -0.5f) ChangeLane(-1);

        // Vertical: up = jump, down = crouch
        if (input.y > 0.5f)
        {
            _jumpPressed = true;
        }
        else if (input.y < -0.5f)
        {
            _crouchPressed = true;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        // If vertical released (was holding down), release crouch
        if (Mathf.Abs(input.y) < 0.5f)
        {
            _crouchReleased = true;
        }
    }

    private void ChangeLane(int direction)
    {
        _currentLane = Mathf.Clamp(_currentLane + direction, -1, 1);
        _targetX = _currentLane * laneDistance;
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        HandleCrouch(dt);
        HandleHorizontal(dt);
        HandleVertical(dt);

        // Ground clamp
        if (controller.isGrounded && _verticalVelocity < 0f)
            _verticalVelocity = -2f;
    }

    private void HandleHorizontal(float dt)
    {
        float newX = Mathf.Lerp(transform.position.x, _targetX, laneChangeSpeed * dt);
        float deltaX = newX - transform.position.x;

        // Snap when very close to avoid infinite tiny lerps
        if (Mathf.Abs(_targetX - transform.position.x) < 0.01f)
            deltaX = _targetX - transform.position.x;

        controller.Move(new Vector3(deltaX, 0f, 0f));
    }

    private void HandleVertical(float dt)
    {
        // Jump (can't jump while crouching)
        if (_jumpPressed && controller.isGrounded && !_isCrouching)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        _jumpPressed = false;

        // Apply gravity
        _verticalVelocity += gravity * dt;

        controller.Move(new Vector3(0f, _verticalVelocity * dt, 0f));
    }

    private void HandleCrouch(float dt)
    {
        // Press down = crouch, release = stand
        if (_crouchPressed)
        {
            _isCrouching = true;
            _crouchPressed = false;
        }
        if (_crouchReleased)
        {
            _isCrouching = false;
            _crouchReleased = false;
        }

        _targetHeight = _isCrouching ? crouchHeight : standHeight;

        float newHeight = Mathf.Lerp(controller.height, _targetHeight, crouchSpeed * dt);
        if (Mathf.Abs(newHeight - _targetHeight) < 0.01f)
            newHeight = _targetHeight;

        controller.height = newHeight;

        // Keep feet anchored: adjust center so the bottom of the capsule stays put
        Vector3 c = controller.center;
        c.y = newHeight / 2f;
        controller.center = c;
    }
}