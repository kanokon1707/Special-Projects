/*using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableEgg : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image eggImage;  // Reference to the Image component of the egg
    private RectTransform rectTransform;  // RectTransform of the egg for positioning
    //private CanvasGroup canvasGroup;  // To control visibility during drag
    private GameController gameController;  // Reference to the GameController

    private Vector2 originalPosition;  // Store the original position of the egg

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
        gameController = FindObjectOfType<GameController>();  // Find the GameController in the scene
    }

    // When dragging starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;  // Store the position where the egg starts
        //canvasGroup.alpha = 0.6f;  // Make the egg semi-transparent while dragging
        //canvasGroup.blocksRaycasts = false;  // Disable raycasts so other objects can receive events
    }

    // While dragging
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position;  // Update egg's position based on the pointer's position
    }

    // When dragging ends (either successfully dropped or canceled)
    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.alpha = 1f;  // Reset the alpha to normal
       // canvasGroup.blocksRaycasts = true;  // Re-enable raycasts

        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("BoardSlot"))
        {
            // If the egg is dropped on a valid board slot, call the GameController method
            int slotIndex = eventData.pointerEnter.GetComponent<BoardSlot>().slotIndex;
            gameController.PlaceEggOnBoard(slotIndex, eggImage);
        }
        else
        {
            // If not dropped on a valid slot, return the egg to its original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}*/

