using UnityEngine;
using System.Collections;

public class RecNumber : MonoBehaviour
{
    private Color _color = new Color(0f, 1f, 0f);
    private float _alpha = 2f;
    private Camera _camera;
    public GUIText _UIText;
    public GameObject _ball;

    // Use this for initialization
    void Start ()
    {
        _camera = Camera.main;
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update ()
    {
        _alpha -= 0.05f;
        _color.a = _alpha;
        _UIText.color = _color;
        Vector3 position = _UIText.transform.position;
        position.y += 0.005f;
        _UIText.transform.position = position;
        if (_alpha < 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Let the number up.
    public void Show(int amount)
    {
        gameObject.SetActive(true);
        _color = new Color(0f, 1f, 0f);
        _alpha = 2f;
        _UIText.text = amount.ToString();
        Vector3 position = _camera.WorldToViewportPoint(_ball.transform.position);
        _UIText.transform.position = position;
    }
}
