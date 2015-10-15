using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BarController : MonoBehaviour, IEventSystemHandler
{
    bool _buttonHeld = false;

    public void Pressed(BaseEventData eventData)
    {
        _buttonHeld = true;
    }

    public void Notpressed(BaseEventData eventData)
    {
        _buttonHeld = false;
    }

    public bool GetButton()
    {
        return _buttonHeld;
    }
}
