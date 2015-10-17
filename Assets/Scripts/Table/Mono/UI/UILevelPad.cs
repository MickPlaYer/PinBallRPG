using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UILevelPad : MonoBehaviour
{
    private const string CURRENT_LEVEL_KEY = "current_level";
    private const string LEVEL_ = "Level ";
    private const string FAIL_STAY = "TRY\nAGAIN";
    private const string FAIL_EXIT = "START\nMENU";
    private const string FAIL_TITLE = " Fail!";
    private const string CLEAR_STAY = "NEXT\nLEVEL";
    private const string CLEAR_EXIT = "START\nMENU";
    private const string CLEAR_TITLE = " Clear!";
    private ItemBox _itemBox;
    private List<GameObject> _itemList = new List<GameObject>();
    private int _gameLevel = 1;
    public HPBar _hpBar;
    public Text _title;
    public Text _stay;
    public Text _exit;

    // Use this for initialization
    void Start()
    {
        _itemBox = new ItemBox();
        _gameLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_stay == null)
            Debug.Log("_stay = null");
        if (_exit == null)
            Debug.Log("_exit == null");
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

    // Add item to item list.
    public void AddItem(GameObject item)
    {
        _itemList.Add(item);
    }

    // Add item from item list to item box.
    public void PickItem(int id, GameObject obj)
    {
        _itemBox.PickUpItem(id);
        _itemList.Remove(obj);
        Destroy(obj);
    }

    // If all items are picked.
    public bool IsAllItemPicked
    {
        get { return _itemList.Count == 0; }
    }

    // Add up level count and save items into PlayerPrefs.
    private void SaveGameChanged()
    {
        _itemBox.SaveItems();
        _gameLevel++;
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, _gameLevel);
        PlayerPrefs.Save();
    }
}
