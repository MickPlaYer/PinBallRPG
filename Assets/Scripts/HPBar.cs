using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour
{
    private Transform _bar;
    public float _scale = 1.0f;

    // Use this for initialization
    void Start()
    {
        _bar = transform.FindChild("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScale(float scale)
    {
        _scale = scale;
        _bar.localScale = new Vector3(scale, 1, 1);
    }
}
