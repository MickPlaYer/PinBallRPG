using UnityEngine;
using System.Collections;

public class RampCollider2D : MonoBehaviour
{
    private PolygonCollider2D _edgeCollider;

    // Use this for initialization
    void Start()
    {
        // Create PolygonCollider2D by code;
        _edgeCollider = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[3];
        points[0] = new Vector2(-0.5f, 0.5f);
        points[1] = new Vector2(0.5f, -0.5f);
        points[2] = new Vector2(-0.5f, -0.5f);
        _edgeCollider.points = points;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
