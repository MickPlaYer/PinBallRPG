using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    public float _velocityDown = 0.90f;
    public float _forceValue = 300f;
    public float _rotateSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.back, _rotateSpeed);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 vector = transform.position - other.transform.position;
        float delta = vector.magnitude;
        vector.Normalize();
        Rigidbody2D rd = other.GetComponent<Rigidbody2D>();
        rd.velocity *= _velocityDown;
        rd.AddForce(vector * _forceValue * (2.5f - delta) / 2.5f, ForceMode2D.Force);
    }
}
