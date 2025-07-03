using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Image healthFillImage;

    public float maxHealth = 100f;
    private float currentHealth;

    private Vector3 lastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        lastPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moved = Vector3.Distance(player.position, lastPosition);

        if (moved > 0)
        {
            currentHealth -= 10f * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        healthFillImage.fillAmount = currentHealth / maxHealth;
        lastPosition = player.position;
    }
}
