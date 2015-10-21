using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    private const float FAST_SPEED = 10f;
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
            PlayAudio(velocity);
            ball.AddForce(-contactNormal * velocity * _value, ForceMode2D.Force);
        }
    }

    // Play audio if speed is fast.
    private void PlayAudio(float speed)
    {
        if (_audio != null)
            if (speed > FAST_SPEED)
                _audio.Play();
    }
}
