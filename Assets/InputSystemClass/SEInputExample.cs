using UnityEngine;
using UnityEngine.InputSystem;

public class SEInputExample : MonoBehaviour
{
    public InputActionAsset inputActions;

    public Vector2 moveInput;
    public bool jumpInput;
    public bool shootInput;

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.FindAction("Jump").performed += OnJumpPerformed;
        inputActions.FindAction("Jump").canceled += OnJumpCanceled;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.FindAction("Jump").performed -= OnJumpPerformed;
        inputActions.FindAction("Jump").canceled -= OnJumpCanceled;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
        shootInput = inputActions.FindAction("Shoot").ReadValue<float>() > 0;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump performed ");
        jumpInput = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Jump canceled ");
        jumpInput = false;
    }
}
