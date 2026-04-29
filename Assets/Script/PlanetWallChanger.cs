using UnityEngine;

public class PlanetWallChanger : MonoBehaviour, IInteractable
{
    public Renderer[] walls;
    public Material[] wallColors;

    public Renderer floorRenderer;
    public Material[] floorMaterials;

    public Light roomLight;
    public Color[] lightColors;

    public Material[] skyboxes;

    private int index = -1;

    public void Interact()
    {
        index++;

        if (wallColors.Length > 0 && index >= wallColors.Length)
            index = 0;

        // Walls
        if (wallColors.Length > 0)
        {
            Material wallMat = wallColors[index % wallColors.Length];

            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i] != null)
                    walls[i].material = wallMat;
            }
        }

        // Floor
        if (floorRenderer != null && floorMaterials.Length > 0)
        {
            floorRenderer.material = floorMaterials[index % floorMaterials.Length];
        }

        // Light
        if (roomLight != null && lightColors.Length > 0)
        {
            roomLight.color = lightColors[index % lightColors.Length];
        }

        // Skybox
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[index % skyboxes.Length];
            DynamicGI.UpdateEnvironment();
        }
    }
}