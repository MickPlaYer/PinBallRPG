using UnityEngine;
using System.Collections;

public class RampCollider2D : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Create PolygonCollider2D by code;
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[3];
        points[0] = new Vector2(-0.5f, 0.5f);
        points[1] = new Vector2(0.5f, -0.5f);
        points[2] = new Vector2(-0.5f, -0.5f);
        collider.points = points;
    }
}
