using UnityEngine;

public class SofaInteract : MonoBehaviour, IInteractable
{
    public Material[] colors;
    private int index = 0;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Interact()
    {
        index++;

        if (index >= colors.Length)
            index = 0;

        rend.material = colors[index];
        Debug.Log("Sofa color changed");
    }
}