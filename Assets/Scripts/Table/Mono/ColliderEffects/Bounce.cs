using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    public float _value = 20f;
    public bool _createAudioSouce = true;
    private AudioSource _audio = null;

    void Start ()
    {
        // Create audio.
        if (_createAudioSouce)
        {
            _audio = gameObject.AddComponent<AudioSource>();
            _audio.clip = Resources.Load("Sounds/Wall") as AudioClip;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Let Ball get a rebound force.
        if (collision.gameObject.name == "Ball")
        {
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 contactNormal = collision.contacts[0].normal;
            float velocity = collision.relativeVelocity.magnitude;
            if (_audio != null)
                if (velocity > 10f)
                    _audio.Play();
            ball.AddForce(-contactNormal * velocity * _value, ForceMode2D.Force);
        }
    }
}
