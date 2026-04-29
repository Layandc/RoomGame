using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);

                IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}