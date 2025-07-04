using UnityEngine;
using TMPro;
using System.Collections;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControls _controls;
    private Vector2 inputMove;
    private float inputRotate;
    [SerializeField] private float speedMove = 10f;
    [SerializeField] private float speedRotate = 100f;

    private bool isOnGround = true, jumpRequested = false;
    [SerializeField] float jumpForce = 10f;

    [SerializeField] TextMeshProUGUI sphereCounterText;
    private RectTransform textRect;
    private int sphereCounter = 0;
    private Vector2 startPos = new Vector2(500f, 220f);

    private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;

    private bool gameEnded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _controls = new PlayerControls();

        _controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => inputMove = Vector2.zero;

        _controls.Player.Rotate.performed += ctx => inputRotate = ctx.ReadValue<float>();
        _controls.Player.Rotate.canceled += ctx => inputRotate = 0f;

        _controls.Player.Jump.performed += ctx => jumpRequested = true;

        textRect = sphereCounterText.GetComponent<RectTransform>();
        textRect.anchoredPosition = startPos;

        audioSource = GetComponent<AudioSource>();        
    }

    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere") && !gameEnded)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            Destroy(other.gameObject);
            sphereCounter++;
            if (sphereCounter < 5)
            {
                sphereCounterText.text = $"Count = {sphereCounter} \nrequire = 5";
            }
            else
            {
                gameEnded = true;

                if (winSound != null) {
                    audioSource.PlayOneShot(winSound);
                }
                sphereCounterText.text = "All spheres collected!";
                StartCoroutine(MoveTexttoCenter());
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (gameEnded) return;
        isOnGround = Physics.Raycast(rb.position, Vector3.down, 0.6f);

        if (isOnGround)
        {
            Vector3 moveDirection = transform.forward * inputMove.y + transform.right * inputMove.x;
            rb.MovePosition(rb.position + moveDirection * speedMove * Time.fixedDeltaTime);

            Quaternion turn = Quaternion.Euler(0f, inputRotate * speedRotate * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turn);
        }       
               
        if (jumpRequested && isOnGround) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        jumpRequested = false;
    }

    private IEnumerator MoveTexttoCenter()
    {
        Vector2 endPos = Vector2.zero;
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            textRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
        Time.timeScale = 0f;
    }
}
