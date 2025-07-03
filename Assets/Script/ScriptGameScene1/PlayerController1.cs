using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android.LowLevel;

public class PlayerController1 : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController controller;
    private Vector2 moveImput;

    private bool isGrounded;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;
    private Vector3 velocity;

    private float moveSpeed = 10f, rotateSpeed = 100f, rotateMove;

    void Awake()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.slopeLimit = 45;

        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0f, 3f, -7f);
        Camera.main.transform.localRotation = Quaternion.Euler(20f, 0f, 0f);

        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => moveImput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveImput = Vector2.zero;
        controls.Player.Rotate.performed += ctx => rotateMove = ctx.ReadValue<float>();
        controls.Player.Rotate.canceled += ctx => rotateMove = 0f;

        controls.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 localDirMove = new Vector3(moveImput.x, 0f, moveImput.y).normalized;
        Vector3 worldMove = transform.TransformDirection(localDirMove);
        controller.Move(worldMove * moveSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * rotateMove * rotateSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
