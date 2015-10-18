using UnityEngine;
using System.Collections;

public class Cover : MonoBehaviour
{
    enum States
    {
        None,
        RedFlashing,
        Down
    }

    private States _state = States.None;
    private HPBar _heroHpBar;
    private SpriteRenderer _sprite;
    private float _flashValue = 0f;
    private float _flashDelta = 0.01f;

    // Use this for initialization
    void Start ()
    {
        _heroHpBar = GameObject.Find("HeroHPBar").GetComponent<HPBar>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (_heroHpBar.Scale >= 0.5f)
            DoClear();
        else if (_heroHpBar.Scale > 0f)
            DoRedFlash(_heroHpBar.Scale);
        else
            DoBlackShutDown();
    }

    // Make cover clear.
    private void DoClear()
    {
        if (_state != States.None)
        {
            _sprite.color = Color.clear;
            _state = States.None;
        }
    }

    // Make the red flash when low hp.
    private void DoRedFlash(float scale)
    {
        _state = States.RedFlashing;
        _flashValue += _flashDelta;
        if (Mathf.Abs(_flashValue) > 0.1f)
            _flashDelta *= -1f;
        float alpha = 0.5f - (scale + _flashValue);
        _sprite.color = new Color(1f, 0f, 0f, alpha);
    }

    // Make cover into back when shut down.
    private void DoBlackShutDown()
    {            
        if (_state != States.Down)
        {
            _state = States.Down;
            _sprite.color = new Color(0f, 0f, 0f, 0.75f);
        }
    }
}
