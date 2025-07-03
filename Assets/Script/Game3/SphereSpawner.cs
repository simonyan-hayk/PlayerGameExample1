using Unity.VisualScripting;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    private float timer;
    private int mask;
    private GameObject sphere;

    private void Awake()
    {
        mask = ~(1 << LayerMask.NameToLayer("Ground"));
    }

    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer > 0.5f)
        {
            sphere = Instantiate(spherePrefab, GetPos(), Quaternion.identity);
            Destroy(sphere, 10f);
            timer = 0f;
        }
    }

    private Vector3 GetPos()
    {
        float x, z;

        for (int i = 0; i < 20; i++) 
        {
            x = Random.Range(-14f, 14f);
            z = Random.Range(-14f, 14f);

            if (Mathf.Abs(x) > 1.5f || Mathf.Abs(z) > 1.5f)
            {
                if (!Physics.CheckSphere(new Vector3(x, 0.5f, z), 0.6f, mask))
                    return new Vector3(x, 0.5f, z);
            }
        }
        return new Vector3(100f, 0f, 100f);
    }
}
