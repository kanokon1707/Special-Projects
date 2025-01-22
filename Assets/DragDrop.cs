using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas; // Reference to the Canvas
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private BoardSlot currentSlot; // Reference to the current board slot the egg is placed in
    private bool isEggPlaced = false; // Flag to check if egg is already placed
    private DragDrop dragDropComponent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        originalParent = transform.parent;
        dragDropComponent = GetComponent<DragDrop>(); // Reference to this DragDrop component
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Allow dragging only if the egg is not placed
        if (!isEggPlaced)
        {
            canvasGroup.alpha = 0.6f; // Make it semi-transparent
            canvasGroup.blocksRaycasts = false; // Allow raycasts to pass through
            transform.SetParent(canvas.transform); // Move to top layer
        }
        else
        {
            canvasGroup.blocksRaycasts = true; // Prevent dragging if egg is already placed
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isEggPlaced)
        {
            // Move the egg with the mouse/cursor
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
{
    // Reset canvasGroup alpha and raycast blocking
    canvasGroup.alpha = 1f;
    canvasGroup.blocksRaycasts = true;

    if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("BoardSlot"))
    {
        BoardSlot boardSlot = eventData.pointerEnter.GetComponent<BoardSlot>();

        if (boardSlot != null)
        {
            // Drop the egg in the slot
            boardSlot.PlaceEgg(gameObject);

            // Mark egg as placed, so it can't be dragged anymore
            currentSlot = boardSlot;
            isEggPlaced = true; // Set flag to prevent further dragging

            var eggImage = GetComponent<Image>();
            if (eggImage != null)
            {
                var eggManager = FindObjectOfType<EggManager>();
                if (eggManager != null)
                {
                    eggManager.MarkEggAsPlaced(eggImage);
                }
            }

            // Disable DragDrop component to fully disable dragging and dropping
            if (dragDropComponent != null)
            {
                dragDropComponent.enabled = false;
            }

            // Check for win after placing the egg
            var eggManagerInstance = FindObjectOfType<EggManager>();
            if (eggManagerInstance != null)
            {
                if (eggManagerInstance.CheckForWin())
                {
                    eggManagerInstance.GameOver();
                }
                else
                {
                    eggManagerInstance.ToggleTurn();
                }
            }
        }
        else
        {
            // If the slot is invalid, return to original position
            ReturnToOriginalParent();
        }
    }
    else
    {
        // If not dropped on a valid slot, return to original position
        ReturnToOriginalParent();
    }
}


    private void ReturnToOriginalParent()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero; // Reset position to its original spot
    }
}
