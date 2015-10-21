using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float _boundTop = 4.0f;
    public float _boundBottom = -3.5f;
    public GameObject _object;

    // Update is called once per frame
    void Update()
    {
        // Avoid camera out of table zone.
        Vector3 cameraPosition = transform.position;
        bool isOutOfTop = _object.transform.position.y > _boundTop;
        bool isOutOfBottom = _object.transform.position.y < _boundBottom;
        if (isOutOfTop)
            cameraPosition.y = _boundTop;
        else if (isOutOfBottom)
            cameraPosition.y = _boundBottom;
        else
            cameraPosition.y = _object.transform.position.y;
        transform.position = cameraPosition;
    }
}
