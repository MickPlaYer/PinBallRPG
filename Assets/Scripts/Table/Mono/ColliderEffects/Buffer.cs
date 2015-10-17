using UnityEngine;
using System.Collections;

public class Buffer : MonoBehaviour
{
    public float _value = 20;
    private AudioSource _audio = null;

    void Start ()
    {
        // Create audio.
        _audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Let Ball get a buffer.
        if (collision.gameObject.name == "Ball")
        {
            if (_audio != null)
                _audio.Play();
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 contactNormal = collision.contacts[0].normal;
            ball.velocity = -contactNormal * _value;
        }
    }
}
