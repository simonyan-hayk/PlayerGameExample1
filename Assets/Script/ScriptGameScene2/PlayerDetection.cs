using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine.UIElements;
public class PlayerDetection : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private float _minimumDistanceForDetected = 10f;
    private float _currentDistanceToPlayer;

    private Vector3 _enemyPosition;
    private Vector3 _ditectionToPlayer;

    [SerializeField] private TextMeshProUGUI _distanceOnDisplay;

    private PlayerControls controls;
    private Vector2 moveImput;
    private float speedMove = 10f;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => moveImput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveImput = Vector2.zero;
    }

    void Start()
    {
        _enemyPosition = transform.position;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    // Update is called once per frame
    void Update()
    {
        _ditectionToPlayer = _player.transform.position - _enemyPosition;
        _currentDistanceToPlayer = _ditectionToPlayer.magnitude;

        _distanceOnDisplay.text = _currentDistanceToPlayer.ToString("0.00") +" m";

        if (_currentDistanceToPlayer < _minimumDistanceForDetected)
            _distanceOnDisplay.color = Color.red;
        else _distanceOnDisplay.color = Color.white;

        _player.transform.position += new Vector3(moveImput.x * speedMove * Time.deltaTime, 0f, moveImput.y * speedMove * Time.deltaTime); 
    }
}
