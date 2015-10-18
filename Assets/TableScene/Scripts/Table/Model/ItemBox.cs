using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox
{
    private const string ITEM_BOX_KEY = "item_box";
    private const string DEFAULT_BOX = "default_item_box";
    private List<Item> _boxList = new List<Item>();

    public ItemBox()
    {
        // Load default from resources
        TextAsset default_item_box = Resources.Load(DEFAULT_BOX) as TextAsset;
        // Get string from PlayerPrefs
        string itemBox = PlayerPrefs.GetString(ITEM_BOX_KEY, default_item_box.text);
        Debug.Log(itemBox);
        var data = JSON.Parse(itemBox);
        // Transfer data to list
        for (int i = 0; i < data.Count; i++)
        {
            int id = data[i]["id"].AsInt;
            int number = data[i]["amount"].AsInt;
            _boxList.Add(new Item(id, number));
        }
    }

    // Add item to data list.
    public void PickUpItem(int id)
    {
        Item item = _boxList.Find(delegate(Item i) { return i.ID == id; });
        if (item != null)
            item.Number++;
        else
            _boxList.Add(new Item(id, 1));
    }

    // Save items to PlayerPrefs.
    public void SaveItems()
    {
        // Create new json array data
        JSONNode data = JSON.Parse("[]");
        // Push list items into json array
        foreach (Item i in _boxList)
        {
            var node = JSON.Parse("{}");
            node["id"].AsInt = i.ID;
            node["amount"].AsInt = i.Number;
            data.Add(node);
        }
        // Save data to PlayerPrefs
        PlayerPrefs.SetString(ITEM_BOX_KEY, data.ToString());
        Debug.Log(data.ToString());
    }
}
