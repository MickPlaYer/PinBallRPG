using UnityEngine;
using System.Collections;

public class PinBallBar : MonoBehaviour
{
    private enum State
    {
        Bottom,
        Upping,
        Top,
        Downing
    }

    private const int MAX_ANGLE = 28;
    private const int MIN_ANGLE = -7;
    private const int UNIT_ANGLE = 7;
    private Vector3 _angles;
    private State _state = State.Bottom;
    private KeyCode _keyCode = KeyCode.Space;
    private BarController _controller = null;
    private AudioSource _audio;

    // Use this for initialization
    void Start()
    {
        SetCollider();
        _angles = transform.eulerAngles;
        _angles.z = MIN_ANGLE;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTopBottom();
        CheckTopHolding();
        Turn();
    }

    // OnCollisionStay2D is called once per frame for every collider2D/rigidbody2D that is touching rigidbody2D/collider2D (2D physics only)
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (_state == State.Upping)
        {
            if (collision.gameObject.name == "Ball")
            {
                Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector2 contactNormal = collision.contacts[0].normal;
                Vector2 relativeVelocity = collision.relativeVelocity;
                float _barValue = (MAX_ANGLE - _angles.z) / UNIT_ANGLE + 40;
                ball.AddForce((-contactNormal * _barValue * 20f) + relativeVelocity, ForceMode2D.Force);
            }
        }
    }

    // Let the bar start turnning up.
    public void GoUp(BarController controller)
    {
        if (_state == State.Bottom)
        {
            _audio.Play();
            _controller = controller;
            _state = State.Upping;
        }
    }

    // Let the bar start turnning up.
    public void GoUp(KeyCode keyCode)
    {
        if (_state == State.Bottom)
        {
            _audio.Play();
            _keyCode = keyCode;
            _state = State.Upping;
        }
    }

    public void ShutDown()
    {
        _state = State.Downing;
    }

    // Set the bar's colloder.
    private void SetCollider()
    {
        PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(0f, 0.5f);
        points[1] = new Vector2(0f, -0.5f);
        points[2] = new Vector2(3.5f, -0.5f);
        points[3] = new Vector2(3.5f, 0f);
        points[4] = new Vector2(3.3f, 0.2f);
        polygonCollider.points = points;
    }

    // Check and refresh the state when the bar is on top or at bottom.
    private void CheckTopBottom()
    {
        if (_state == State.Upping && _angles.z >= MAX_ANGLE)
        {
            _angles.z = MAX_ANGLE;
            _state = State.Top;
        }
        else if (_state == State.Downing && _angles.z <= MIN_ANGLE)
        {
            _angles.z = MIN_ANGLE;
            _state = State.Bottom;
        }
    }

    // Check if key is holding when the bar is on top, if not the state go downing.
    private void CheckTopHolding()
    {
        if (_controller != null)
        {
            if (_state == State.Top && !_controller.GetButton())
                _state = State.Downing;
        }
        else
        {
            if (_state == State.Top && !Input.GetKey(_keyCode))
                _state = State.Downing;
        }
    }

    // Let the bar turn up or down.
    private void Turn()
    {
        if (_state == State.Upping)
            _angles.z += UNIT_ANGLE;
        else if (_state == State.Downing)
            _angles.z -= UNIT_ANGLE;
        transform.eulerAngles = _angles;
    }
}
