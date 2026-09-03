using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public InputActionAsset inputActions;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 15f;


    [Header("Readonly Inputs")]
    public Vector2 moveInput;


    public void OnEnable()
    {
        inputActions.Enable();


        // register Jump Action
        inputActions.FindAction("Jump").performed += OnJumpPerformed;
    }


    public void OnDisable()
    {
        inputActions.Disable();
        // unregister Jump Action
        inputActions.FindAction("Jump").performed -= OnJumpPerformed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = inputActions.FindAction("Move").ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Converting the 2D moveInput into a 3D direction vector for movement
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);

        // Applying force to the Rigidbody in the direction of movement
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * moveSpeed); // Adjust speed as needed

    }


    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
