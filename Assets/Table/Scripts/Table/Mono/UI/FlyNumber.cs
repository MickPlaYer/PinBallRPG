using UnityEngine;
using System.Collections;

public class FlyNumber : MonoBehaviour
{
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
        Vector2 velocity = _rigidbody.velocity;
        if (velocity.y > 0)
            _UIText.fontSize += 1;
        else
            _UIText.fontSize -= 6;
        if (_UIText.fontSize <= 0)
        {
            gameObject.SetActive(false);
        }
        Vector3 position = _camera.WorldToViewportPoint(_rigidbody.transform.position);
        _UIText.transform.position = position;
    }

    // Let the number fly.
    public void StartFly(Vector2 from, int damage)
    {
        gameObject.SetActive(true);
        _rigidbody.transform.localPosition = Vector3.zero;
        _rigidbody.velocity = new Vector2(2f, 3f);
        _UIText.fontSize = 50;
        _UIText.text = damage.ToString();
        transform.position = from;
    }
}
