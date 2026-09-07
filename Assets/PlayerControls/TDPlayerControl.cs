using UnityEngine;

public class TDPlayerControl : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    [Header("Dash Settings")]
    public float dashDistance = 5f;      // how far the dash travels, total
    public float dashDuration = 0.25f;   // how long it takes to cover that distance
    public float dashCooldown = 1f;      // time after dash ends before another can start
    public LayerMask dashObstacleMask;   // walls/obstacles the dash should not pass through
    public AnimationCurve dashCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public float dashForce = 10f; // force applied to objects when dashing through them

    private Vector2 _moveInput;
    private Vector2 _cursorPosition;
    private bool _dashInput;

    private bool _isDashing;
    private Vector3 _dashStartPosition;
    private Vector3 _dashTargetPosition;
    private float _dashStartTime;
    private float _dashCooldownEndTime;

    void Awake()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    public void FixedUpdate()
    {
        // rotate the player to face the cursor position
        Vector3 point = TPUserInput.ThreeDWorldPositionFromScreen(_cursorPosition, Camera.main);
        Vector3 lookDirection = point - transform.position;
        lookDirection.y = 0;
        if (lookDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        // apply gravity
        if (!controller.isGrounded)
        {
            controller.Move(Physics.gravity * Time.fixedDeltaTime);
        }

        if (_isDashing)
        {
            UpdateDash();
        }
        else
        {
            // normal movement only happens when not dashing
            Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
            controller.Move(move * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateDash()
    {
        float elapsed = Time.time - _dashStartTime;
        float t = Mathf.Clamp01(elapsed / dashDuration);
        float easedT = dashCurve.Evaluate(t);

        Vector3 desiredPosition = Vector3.Lerp(_dashStartPosition, _dashTargetPosition, easedT);
        Vector3 delta = desiredPosition - transform.position;
        controller.Move(delta);

        if (t >= 1f)
        {
            _isDashing = false;
            _dashCooldownEndTime = Time.time + dashCooldown;
        }
    }

    private void TryStartDash()
    {
        if (_isDashing || Time.time < _dashCooldownEndTime)
        {
            return;
        }

        Vector3 direction = transform.forward;
        float travelDistance = dashDistance;

        // prevent dashing through walls/obstacles
        float castRadius = controller.radius * 0.9f; // slightly smaller to avoid false hits
        if (Physics.CapsuleCast(
                GetCapsuleTop(), GetCapsuleBottom(), castRadius,
                direction, out RaycastHit hit, dashDistance, dashObstacleMask))
        {
            travelDistance = Mathf.Max(0f, hit.distance - 0.1f); // small buffer from the wall
        }

        _dashStartPosition = transform.position;
        _dashTargetPosition = _dashStartPosition + direction * travelDistance;
        _dashStartTime = Time.time;
        _isDashing = true;
    }

    private Vector3 GetCapsuleTop()
    {
        return transform.position + Vector3.up * (controller.height - controller.radius);
    }

    private Vector3 GetCapsuleBottom()
    {
        return transform.position + Vector3.up * controller.radius;
    }

    public void OnMoveInput(Vector2 moveInput)
    {
        _moveInput = moveInput;
    }

    public void OnCursorMoveInput(Vector2 cursorPosition)
    {
        _cursorPosition = cursorPosition;
    }

    public void OnDashInput(bool dashInput)
    {
        _dashInput = dashInput;

        if (dashInput)
        {
            TryStartDash();
        }
    }

    public void OnTriggeredByEvent(GameObject other)
    {
        if (_isDashing)
        {
            // get rigidbody of the other object if it has one
            Rigidbody otherRb = other.GetComponent<Rigidbody>();

            if (otherRb != null)
            {
                // apply a force to the other object in the direction of the dash
                Vector3 dashDirection = transform.forward;
                float forceMagnitude = dashForce;
                otherRb.AddForce(dashDirection * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}