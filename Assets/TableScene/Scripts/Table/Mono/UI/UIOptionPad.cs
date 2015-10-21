using UnityEngine;
using System.Collections;

public class UIOptionPad : MonoBehaviour
{
    public GameObject _levelPad;
    public GameObject[] _hideObject;

    // Use this for initialization
    void Start()
    {
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
        foreach (GameObject obj in _hideObject)
            obj.SetActive(false);
        gameObject.SetActive(true);
    }

    // Close itself.
    public void Close()
    {
        Time.timeScale = 1f;
        foreach (GameObject obj in _hideObject)
            obj.SetActive(true);
        gameObject.SetActive(false);
    }

    // Back to title.
    public void BackToTitle()
    {
        Time.timeScale = 1f;
        _levelPad.GetComponent<UILevelPad>().LoadMenuScene();
    }
}
