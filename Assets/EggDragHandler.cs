using UnityEngine;
using UnityEngine.EventSystems;

public class EggDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;  // To control opacity and raycast behavior during drag
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        // Get the CanvasGroup and RectTransform components
        //canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // When dragging starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;  // Save the original position
        //canvasGroup.alpha = 0.6f;  // Set the opacity to make the egg semi-transparent
       // canvasGroup.blocksRaycasts = false;  // Disable raycasts while dragging
    }

    // While dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the egg to follow the pointer (mouse or touch)
        rectTransform.anchoredPosition = eventData.position;
    }

    // When dragging ends
    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the opacity and raycast settings after drag ends
       // canvasGroup.alpha = 1f;  // Restore the opacity to full visibility
        //canvasGroup.blocksRaycasts = true;  // Re-enable raycasts

        // If the egg wasn't dropped on a valid board slot, return it to the original position
        if (eventData.pointerEnter == null || !eventData.pointerEnter.CompareTag("BoardSlot"))
        {
            rectTransform.anchoredPosition = originalPosition;  // Return the egg to its original position
        }
    }
}
