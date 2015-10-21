using UnityEngine;
using System.Collections;

public class FlyNumber : MonoBehaviour
{
    private const int BASIC_FONT_SIZE = 50;
    private const int TEXT_UP = 1;
    private const int TEXT_DOWN = 6;
    private Camera _camera;
    public Rigidbody2D _rigidbody;
    public GUIText _UIText;

    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        ChangeTextSize();
        Vector3 position = _camera.WorldToViewportPoint(_rigidbody.transform.position);
        _UIText.transform.position = position;
    }

    // Change text size by its motion.
    private void ChangeTextSize()
    {
        Vector2 velocity = _rigidbody.velocity;
        if (velocity.y > 0f)
            _UIText.fontSize += TEXT_UP;
        else
            _UIText.fontSize -= TEXT_DOWN;
        if (_UIText.fontSize <= 0)
            gameObject.SetActive(false);
    }

    // Let the number fly.
    public void StartFly(Vector2 position, int damage)
    {
        gameObject.SetActive(true);
        _rigidbody.transform.localPosition = Vector3.zero;
        _rigidbody.velocity = new Vector2(2f, 3f);
        _UIText.fontSize = BASIC_FONT_SIZE;
        _UIText.text = damage.ToString();
        transform.position = position;
    }
}
