using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
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
        vector.Normalize();
        Rigidbody2D rd = other.GetComponent<Rigidbody2D>();
        rd.velocity *= _velocityDown;
        rd.AddForce(vector * _forceValue * (2.5f - delta) / 2.5f, ForceMode2D.Force);
    }
}
