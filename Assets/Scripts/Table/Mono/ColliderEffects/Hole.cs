using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    public float velocity_down = 0.90f;
    public float force_value = 300;

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 vector = transform.position - other.transform.position;
        float delta = vector.magnitude;
        vector.Normalize();
        Rigidbody2D rd = other.GetComponent<Rigidbody2D>();
        rd.velocity *= velocity_down;
        rd.AddForce(vector * force_value * (2 - delta) / 2, ForceMode2D.Force);
    }
}
