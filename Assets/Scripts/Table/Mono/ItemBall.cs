using UnityEngine;
using System.Collections;

public class ItemBall : MonoBehaviour
{
    public PinBallTable _table;
    public Hole _hole;
    private Rigidbody2D _rigidbody = null;
    private BoxCollider2D _collider = null;
    private int _count = 0;

    // Use this for initialization
    void Start()
    {
        _table = GameObject.Find("Table").GetComponent<PinBallTable>();
        _hole = GameObject.Find("Hole").GetComponent<Hole>();
        float torque = Random.Range(100, 1000);
        float sign = Mathf.Sign(Random.value - 0.5f);
        Vector2 vector = Random.insideUnitCircle;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddTorque(torque * sign, ForceMode2D.Force);
        _rigidbody.AddForce(vector * 100, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (_collider == null)
        {
            _count++;
            if (_count > 100)
            {
                _collider = gameObject.AddComponent<BoxCollider2D>();
                _collider.isTrigger = true;
            }
        }
        else
        {
            Vector2 vector = _hole.transform.position - transform.position;
            if (vector.magnitude < 0.1f)
            {
                _table.PickItem(1, gameObject);
            }
        }
    }
}
