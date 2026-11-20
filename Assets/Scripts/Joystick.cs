using UnityEngine;
using UnityEngine.EventSystems;

// on screen joystick
// - dead zone in the middle so small touches do nothing
// - snaps back to the center when you let go (from the week 9 slides)
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float deadZone = 0.2f;

    private Vector2 input = Vector2.zero;

    // the player reads these
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // where is the finger inside the joystick circle
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, eventData.pressEventCamera, out localPoint);

        Vector2 radius = background.sizeDelta / 2f;
        input = new Vector2(localPoint.x / radius.x, localPoint.y / radius.y);

        // cant go outside the circle
        if (input.magnitude > 1f) input = input.normalized;

        // dead zone
        if (input.magnitude < deadZone) input = Vector2.zero;

        // move the small circle with the finger
        handle.anchoredPosition = new Vector2(input.x * radius.x, input.y * radius.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // snap back to the middle
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
