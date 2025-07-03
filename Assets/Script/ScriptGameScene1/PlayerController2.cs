using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private GameObject sphere;

    public void Initialize()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0f, 17f, 0f);
        sphere.AddComponent<Rigidbody>();

        Camera.main.transform.parent = sphere.transform;
        Camera.main.transform.localPosition = new Vector3(0f, 3f, -7f);
        Camera.main.transform.localRotation = Quaternion.Euler(20f, 0f, 0f);
    }
}
