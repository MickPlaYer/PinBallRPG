using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour
{
    private Transform _bar;
    private float _scale = 1.0f;

    // Use this for initialization
    void Start()
    {
        _bar = transform.FindChild("Bar");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void SetScale(float scale)
    {
        if (scale < 0)
            _scale = 0;
        else
            _scale = scale;
        _bar.localScale = new Vector3(_scale, 1, 1);
    }

    public float Scale
    {
        get { return _scale; }
    }
}
