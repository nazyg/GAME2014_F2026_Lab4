using UnityEngine;
using UnityEngine.EventSystems;

// jump button, uses pointer down so the jump happens right when you touch
// (a normal Button only fires when you let go)
public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    private bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    // returns true once per press
    public bool WasPressed()
    {
        if (pressed)
        {
            pressed = false;
            return true;
        }
        return false;
    }
}
