using UnityEngine;
using System.Collections;

public class ComboCount : MonoBehaviour
{
    private const string SHOCK = "shock";
    private ComboBox _comboBox;
    private bool _isVibratable = true;

    // Use this for initialization
    void Start()
    {
        _isVibratable = PlayerPrefs.GetInt(SHOCK) == 1;
        _comboBox = GameObject.Find("ComboShow").GetComponent<ComboBox>(); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Add combo count if hit by ball.
        if (collision.gameObject.name == "Ball")
        {
            _comboBox.AddComboNumber();
            if (_isVibratable)
                Handheld.Vibrate();
        }
    }
}
