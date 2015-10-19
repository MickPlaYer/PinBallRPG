using UnityEngine;
using System.Collections;

public class ItemBall : MonoBehaviour
{
    private int _id;
    private UILevelPad _levelPad;
    private Hole _hole;
    private Rigidbody2D _rigidbody = null;
    private BoxCollider2D _collider = null;
    private int _timeCount = 0;

    // Use this for initialization
    void Start()
    {
        // Set random motion at start.
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
            _timeCount++;
            if (_timeCount > 100)
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
                _levelPad.PickItem(_id, gameObject);
            }
        }
    }

    // Set the level pad and hole.
    public void SetRelatedObject(UILevelPad levelPad, Hole hole)
    {
        _levelPad = levelPad;
        _hole = hole;
    }

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }
}
