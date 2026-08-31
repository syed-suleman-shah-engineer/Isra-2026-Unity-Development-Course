using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform bulletPoint;

    [Header("Input Reference")]
    public InputActionAsset inputActionAsset;
    // public InputActionReference moveInput;
    public InputActionReference fireInput;

    public float moveSpeed = 10;

    public Bullet bulletPrefab;


    [Header("Runtime")]
    public Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        inputActionAsset.Enable();
        fireInput.action.performed += Fire;
    }

    private void OnDisable()
    {
        inputActionAsset.Disable();
        fireInput.action.performed -= Fire;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = inputActionAsset["Move"].ReadValue<Vector2>();

        Vector3 movement = transform.right * input.x
                        + transform.forward * input.y;

        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        var bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        bullet.Launch(bulletPoint.forward);
        Debug.Log("Fire action performed!");
    }


}
