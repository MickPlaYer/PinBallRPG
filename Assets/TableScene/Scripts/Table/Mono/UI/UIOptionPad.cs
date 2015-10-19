using UnityEngine;
using System.Collections;

public class UIOptionPad : MonoBehaviour
{
    public GameObject _levelPad;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        gameObject.SetActive(false);
    }

    // Show itself.
    public void Show()
    {
        if (_levelPad.activeSelf)
            return;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    // Close itself.
    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    // Close itself.
    public void BackToTitle()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("MainScene");
    }
}
