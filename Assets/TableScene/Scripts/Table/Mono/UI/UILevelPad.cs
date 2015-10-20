using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelPad : MonoBehaviour
{
    private const string CURRENT_LEVEL_KEY = "current_level";
    private const string LEVEL_ = "關卡 ";
    private const string FAIL_STAY = "再試\n一次";
    private const string FAIL_EXIT = "回到\n選單";
    private const string FAIL_TITLE = " 失敗！";
    private const string CLEAR_STAY = "到下\n一關";
    private const string CLEAR_EXIT = "回到\n選單";
    private const string CLEAR_TITLE = " 通過！";
    private ItemBox _itemBox;
    private ItemList _itemList;
    private List<int> _pickedIDList = new List<int>();
    private int _gameLevel = 1;
    public HPBar _hpBar;
    public Text _title;
    public Text _stay;
    public Text _exit;

    // Use this for initialization
    void Start()
    {
        _itemBox = new ItemBox();
        _itemList = new ItemList();
        _gameLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        gameObject.SetActive(false);
    }

    // Show the pad when level fail.
    public void ShowFailPad()
    {
        if (gameObject.activeSelf)
            return;
        // Set texts.
        _title.text = LEVEL_ + _gameLevel + FAIL_TITLE;
        _stay.text = FAIL_STAY;
        _exit.text = FAIL_EXIT;
        _hpBar.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Show the pad when level clear.
    public void ShowSuccessPad()
    {
        if (gameObject.activeSelf)
            return;
        // Set texts.
        _title.text = LEVEL_ + _gameLevel + CLEAR_TITLE;
        _stay.text = CLEAR_STAY;
        _exit.text = CLEAR_EXIT;
        SaveGameChanged();
        // Hide hp bar and make cover into black.
        _hpBar.Scale = 0f;
        _hpBar.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Load the table scene.
    public void LoadTableScene()
    {
        Application.LoadLevel("PinBallTable");
    }

    // Load the menu scene.
    public void LoadMenuScene()
    {
        Application.LoadLevel(0);
    }

    // Add up level count and save items into PlayerPrefs.
    private void SaveGameChanged()
    {
        foreach(int id in _pickedIDList)
            _itemBox.GetReward(id);
        _itemBox.SaveReward(_gameLevel);
        _gameLevel++;
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, _gameLevel);
        PlayerPrefs.Save();
    }

    public int MaxItemID
    {
        get { return _itemList.MaxID; }
    }

    public List<int> PickedIDList
    {
        set { _pickedIDList = value; }
    }
}
