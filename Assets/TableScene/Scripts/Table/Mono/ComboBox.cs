using UnityEngine;
using System.Collections;

public class ComboBox : MonoBehaviour
{
    enum State
    {
        Counting,
        Max,
        Cencle,
        MaxCencle
    }

    public Sprite[] _numbers;
    public Sprite[] _max;
    public Sprite[] _combo;
    public SpriteRenderer _numberRenderer;
    public SpriteRenderer _maxRenderer;
    public SpriteRenderer _comboRenderer;
    private int _comboNumber = 0;
    private int _cancleNumber = 0;
    private int _secondCount = 0;
    private float _deltaTimeCount = 0f;
    private bool _isLight = false;
    private State _state = State.Counting;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_state >= State.Cencle)
        {
            _deltaTimeCount += Time.deltaTime;
            if (_deltaTimeCount > 1f)
            {
                if (_isLight)
                {
                    _numberRenderer.sprite = _numbers[_cancleNumber];
                }
                else
                {
                    _numberRenderer.sprite = _numbers[0];
                }
                _isLight = !_isLight;
                _deltaTimeCount -= 1f;
            }
        }
    }

    // Add number and reflash renderer.
    public void AddComboNumber()
    {
        if (_comboNumber < 10)
        {
            _comboNumber++;
            if (_state < State.Cencle)
                _numberRenderer.sprite = _numbers[_comboNumber];
        }
        else
        {
            ShowMaxCombo();
        }
    }

    // Show max combo view.
    public void ShowMaxCombo()
    {
        _state = State.Max;
    }

    // Cancle the combo count.
    public void CancleCombo()
    {
        if (_state > State.Max)
            return;
        _cancleNumber = _comboNumber;
        _comboNumber = 0;
        _state += 2;
        // _numberRenderer.sprite = _numbers[_comboNumber];
    }
}
