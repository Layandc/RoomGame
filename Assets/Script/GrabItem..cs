using UnityEngine;

public class GrabItem : MonoBehaviour, IInteractable
{
    public Transform handPoint;
    private bool isHeld = false;
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (!isHeld)
            PickUpItem();
        else
            DropItem();
    }

    public void PickUpItem()
    {
        isHeld = true;

        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // لا تعطل الكوليدر بالكامل
        // إذا بدك تمنع اصطدامه مع اللاعب، استخدم layers أو IgnoreCollision
    }

    public void DropItem()
    {
        isHeld = false;

        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}