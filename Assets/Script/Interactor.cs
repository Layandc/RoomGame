using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange = 3f;

    private GrabItem heldItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // إذا اللاعب ماسك جسم، افلته مباشرة
            if (heldItem != null)
            {
                heldItem.DropItem();
                heldItem = null;
                return;
            }

            Ray ray = new Ray(interactorSource.position, interactorSource.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);

                GrabItem grabItem = hitInfo.collider.GetComponent<GrabItem>();
                if (grabItem != null)
                {
                    grabItem.PickUpItem();
                    heldItem = grabItem;
                    return;
                }

                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}