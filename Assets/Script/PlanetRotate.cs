using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float rotateSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
    }
}