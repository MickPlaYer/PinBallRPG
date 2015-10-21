using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BarController : MonoBehaviour, IEventSystemHandler
{
    private bool _isButtonHeld = false;

    public void Pressed(BaseEventData eventData)
    {
        _isButtonHeld = true;
    }

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
