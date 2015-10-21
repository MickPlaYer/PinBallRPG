using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public AudioSource _audio;

    public void Cast(Vector2 direction)
    {
        _audio.Play();
        _rigidbody.AddForce(direction * 3000);
    }
}
