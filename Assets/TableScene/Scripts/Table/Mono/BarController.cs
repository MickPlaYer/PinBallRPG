using UnityEngine;
using UnityEngine.EventSystems;

public class BarController : MonoBehaviour, IEventSystemHandler
{
    private bool _isButtonHeld = false;

    // The funtion call by IEventSystemHandler.
    public void Pressed(BaseEventData eventData)
    {
        _isButtonHeld = true;
    }

    // The funtion call by IEventSystemHandler.
    public void Notpressed(BaseEventData eventData)
    {
        _isButtonHeld = false;
    }

    public bool IsButtonHeld
    {
        get { return _isButtonHeld; }
        set { _isButtonHeld = value; }
    }
}
