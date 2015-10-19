using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox
{
    private const string ITEM_BOX_KEY = "item_box";
    private const string HERO_DATA_KEY = "_hero_data";
    private const string DEFAULT_BOX = "default_item_box";
    private List<ItemData> _boxList = new List<ItemData>();

    public ItemBox()
    {
        // Load default from resources
        TextAsset default_item_box = Resources.Load(DEFAULT_BOX) as TextAsset;
        // Get string from PlayerPrefs
        string itemBox = PlayerPrefs.GetString(ITEM_BOX_KEY, default_item_box.text);
        var data = JSON.Parse(itemBox);
        // Transfer data to list
        for (int i = 0; i < data.Count; i++)
        {
            int id = data[i]["id"].AsInt;
            int number = data[i]["amount"].AsInt;
            _boxList.Add(new ItemData(id, number));
        }
    }

    // Add item to data list.
    public void PickUpItem(int id)
    {
        ItemData item = _boxList.Find(delegate(ItemData i) { return i.ID == id; });
        if (item != null)
            item.Amount++;
        else
            _boxList.Add(new ItemData(id, 1));
    }

    // Save money and items.
    public void SaveReward(int gameLevel)
    {
        SaveMoney(gameLevel);
        SaveItems();
    }

    // Add up money to PlayerPrefs.
    private void SaveMoney(int gameLevel)
    {
        string json = PlayerPrefs.GetString(HERO_DATA_KEY);
        JSONNode data = JSON.Parse(json);
        if (data["money"] == null)
            return;
        data["money"].AsInt = data["money"].AsInt += gameLevel * Random.Range(5, 11);
        PlayerPrefs.SetString(HERO_DATA_KEY, data.ToString());
    }

    // Save items to PlayerPrefs.
    private void SaveItems()
    {
        // Create new json array data
        JSONNode data = JSON.Parse("[]");
        // Push list items into json array
        foreach (ItemData i in _boxList)
        {
            var node = JSON.Parse("{}");
            node["id"].AsInt = i.ID;
            node["amount"].AsInt = i.Amount;
            data.Add(node);
        }
        // Save data to PlayerPrefs
        PlayerPrefs.SetString(ITEM_BOX_KEY, data.ToString());
    }
}
