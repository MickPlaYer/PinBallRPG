using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    public CircleCollider2D _collider;
    public float _velocityDown = 0.90f;
    public float _forceValue = 300f;
    public float _rotateSpeed = 10f;

    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.back, _rotateSpeed * Time.deltaTime * 60f);
    }

    // Make attraction
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Projectile" || other.tag == "Skill")
            return;
        Vector2 vector = transform.position - other.transform.position;
        float delta = vector.magnitude;
        float rangeValue = (_collider.radius - delta) / _collider.radius;
        Vector2 normal = vector.normalized;
        Rigidbody2D rd = other.GetComponent<Rigidbody2D>();
        rd.velocity *= _velocityDown;
        rd.AddForce(normal * _forceValue * rangeValue, ForceMode2D.Force);
    }
}
