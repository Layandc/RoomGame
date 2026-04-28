using UnityEngine;

public class UmbrellaPickup : MonoBehaviour, IInteractable
{
    public Transform handPoint;

    public void Interact()
    {
        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }
}