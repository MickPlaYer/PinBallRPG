using UnityEngine;
using System.Collections;

public class ComboBox : MonoBehaviour
{
    enum State
    {
        Down,
        Counting,
        Max,
        Cencle,
        MaxCencle
    }

    const float LIGHT_DELTA = 0.5f;
    const int LIGHT_TIME = 5;
    public Sprite[] _numbers;
    public Sprite[] _max;
    public Sprite[] _combo;
    public Sprite _bubblesDark;
    public SpriteRenderer _numberRenderer;
    public SpriteRenderer _maxRenderer;
    public SpriteRenderer _comboRenderer;
    private int _comboNumber = 0;
    private int _cancleNumber = 0;
    private int _secondCount = 0;
    private float _deltaTimeCount = 0f;
    private bool _isLight = false;
    private State _state = State.Counting;

    // Update is called once per frame
    void Update()
    {
        if (_state >= State.Cencle)
        {
            _deltaTimeCount += Time.deltaTime;
            if (_deltaTimeCount > LIGHT_DELTA)
                DoLightUpDown();
            if (_secondCount >= LIGHT_TIME)
                StopLight();
        }
    }

    // Stop the light action.
    private void StopLight()
    {
        _numberRenderer.sprite = _numbers[0];
        _comboRenderer.sprite = _combo[1];
        _maxRenderer.sprite = _max[1];
        _secondCount = 0;
        _state = State.Counting;
    }

    // Make the light up and down.
    private void DoLightUpDown()
    {
        int numberIndex = _isLight ? _cancleNumber : 0;
        int lightIndex = _isLight ? 0 : 1;
        _numberRenderer.sprite = _numbers[numberIndex];
        _comboRenderer.sprite = _combo[lightIndex];
        if (_state == State.MaxCencle)
            _maxRenderer.sprite = _max[lightIndex];
        _isLight = !_isLight;
        _deltaTimeCount -= LIGHT_DELTA;
        _secondCount++;
    }

    // Add number and reflash renderer.
    public void AddComboNumber()
    {
        if (_comboNumber < 10)
        {
            _comboNumber++;
            if (_state == State.Counting)
            {
                _comboRenderer.sprite = _combo[0];
                _numberRenderer.sprite = _numbers[_comboNumber];
            }
        }
        else
        {
            ShowMaxCombo();
        }
    }

    // Show max combo view.
    public void ShowMaxCombo()
    {
        _maxRenderer.sprite = _max[0];
        _state = State.Max;
    }

    // Cancle the combo count.
    public void CancleCombo()
    {
        if (_state > State.Max || _comboNumber == 0)
            return;
        _cancleNumber = _comboNumber;
        _comboNumber = 0;
        _state += 2;
    }

    // Return current combo count.
    public int Count
    {
        get { return _comboNumber; }
    }

    // Stop the animation.
    public void ShutDown()
    {
        _state = State.Down;
        _numberRenderer.sprite = _numbers[0];
        _comboRenderer.sprite = _combo[1];
        _maxRenderer.sprite = _max[1];
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _bubblesDark;
    }
}
