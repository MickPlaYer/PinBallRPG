using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float _boundTop = 4.0f;
    public float _boundBottom = -3.5f;
    public GameObject _object;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Avoid camera out of table zone.
        Vector3 camera = transform.position;
        bool isOutOfTop = _object.transform.position.y > _boundTop;
        bool isOutOfBottom = _object.transform.position.y < _boundBottom;
        if (isOutOfTop)
            camera.y = _boundTop;
        else if (isOutOfBottom)
            camera.y = _boundBottom;
        else
            camera.y = _object.transform.position.y;
        transform.position = camera;
    }
}
