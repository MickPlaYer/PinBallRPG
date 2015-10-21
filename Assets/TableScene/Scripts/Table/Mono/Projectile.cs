using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private GameObject _target;
    private int _damage;
    private Vector2 _direction;
    public Rigidbody2D _rigidbody;
    public float angularVelocity;
    public PinBallTable _table;

    // Use this for initialization
    void Start()
    {
        _rigidbody.angularVelocity = 500;
        _rigidbody.velocity = _direction *= 10;
    }

    // Check hurt someone.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Wall")
        {
            if (other.gameObject == _target)
            {
                HurtTarget();
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Set the target.
    public void SetTarget(GameObject target)
    {
        _target = target;
        _direction = target.transform.position - transform.position;
        _direction.Normalize();
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    // Hurt target.
    private void HurtTarget()
    {
        if (_target.name == "Ball")
        {
            _table.HurtHero(_damage);
        }
        else if (_target.name == "Boss")
        {
            _table.HurtBoss(_damage);
        }
        gameObject.SetActive(false);
    }
}
