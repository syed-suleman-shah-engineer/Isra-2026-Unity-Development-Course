using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TPUserInput : MonoBehaviour
{
    [Header("References")]
    public InputActionAsset inputActions;


    [Header("Events")]
    public UnityEvent<Vector2> onMove;
    public UnityEvent<Vector2> onCursorMove;
    public UnityEvent<bool> onDash;

    [Header("Runtime Debug")]
    public Vector2 moveInput;
    public Vector2 cursorPosition;
    public bool dashInput;

    private Vector2 _lastMousePosition;

    private void OnEnable()
    {
        inputActions.Enable();

        // Subscribe to the dash action
        inputActions.FindAction("Dash").performed += OnDashPerformed;
        inputActions.FindAction("Dash").canceled += OnDashCanceled;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        // Unsubscribe from the dash action
        inputActions.FindAction("Dash").performed -= OnDashPerformed;
        inputActions.FindAction("Dash").canceled -= OnDashCanceled;
    }


    void Update()
    {
        Vector2 moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
        onMove.Invoke(moveInput);
        this.moveInput = moveInput;

        // use mouse position for cursor movement
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 cursorPosition = mouse.position.ReadValue();

            if (cursorPosition != _lastMousePosition)
            {
                onCursorMove.Invoke(cursorPosition);
                this.cursorPosition = cursorPosition;
                _lastMousePosition = cursorPosition;
            }
        }
    }


    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        // debug start press hold and release
        onDash.Invoke(true);
        this.dashInput = true;
    }

    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        onDash.Invoke(false);
        this.dashInput = false;
    }


    public static Vector3 ThreeDWorldPositionFromScreen(Vector2 screenPosition, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

}
