using UnityEngine;
using System.Collections;

public class Buffer : MonoBehaviour
{
    public float _value = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 contactNormal = collision.contacts[0].normal;
            ball.velocity = -contactNormal * _value;
        }
    }
}
