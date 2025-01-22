using UnityEngine;

public class ChangeSortingLayer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // Assign the sprite renderer in the inspector
    public string Background;     // The new sorting layer you want to switch to

    public void ChangeLayer()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = Background;
        }
        else
        {
            Debug.LogError("SpriteRenderer is not assigned.");
        }
    }
}

