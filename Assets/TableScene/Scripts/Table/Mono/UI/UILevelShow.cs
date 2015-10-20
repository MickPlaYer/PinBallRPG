using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILevelShow : MonoBehaviour
{
    private const string CURRENT_LEVEL_KEY = "current_level";
    private const int WIDTH = 300;
    private const float WAIT_TIME = 1.5f;
    public Text _text;
    public RectTransform _rectTransform;
    private Vector2 _position;
    private bool _isAtMiddle = false;
    private float _waitCount = 0f;
    private AudioSource[] _audios;
    public GameObject _optionButton;
    public GameObject[] _hideObjects;

    // Use this for initialization
    void Start()
    {
        _audios = GetComponents<AudioSource>();
        foreach (var obj in _hideObjects)
            obj.SetActive(false);
        Time.timeScale = 0f;
        int gameLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
        _text.text = "關卡 " + gameLevel;
        _rectTransform = GetComponent<RectTransform>();
        _position = new Vector2((WIDTH + Screen.width) / 2f, 0f);
        _rectTransform.anchoredPosition = _position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAtMiddle && _waitCount < WAIT_TIME)
        {
            WaitTimeCount();
            return;
        }
        _position.x -= Screen.width / 20;
        OnMiddle();
        if (_position.x <= -(WIDTH + Screen.width) * 2)
            gameObject.SetActive(false);
        _rectTransform.anchoredPosition = _position;
    }

    // Play ready sound at middle. 
    private void OnMiddle()
    {
        if (_position.x <= 0 && !_isAtMiddle)
        {
            // Audio: "Ready?"
            _audios[0].Play();
            _position.x = 0;
            _isAtMiddle = true;
        }
    }

    // Wait for time count.
    private void WaitTimeCount()
    {
        _waitCount += Time.unscaledDeltaTime;
        // Audio: "Go!"
        if (_waitCount >= WAIT_TIME)
        {
            _audios[1].Play();
            foreach (var obj in _hideObjects)
                obj.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}