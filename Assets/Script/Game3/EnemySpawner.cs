using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject sphereSpawnerObject;

    private SphereSpawner sphereSpawner;
    private float timer = 0f;

    private void Start()
    {
        sphereSpawner = sphereSpawnerObject.GetComponent<SphereSpawner>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            Instantiate(enemyPrefab, sphereSpawner.GetPos(), Quaternion.identity);
            timer = 0f;
        }
    }
}