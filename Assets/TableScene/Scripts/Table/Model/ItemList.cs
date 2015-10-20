using SimpleJSON;
using System.Collections;
using UnityEngine;

class ItemList
{
    private const string ITEM_LIST = "item_list";
    private JSONNode _itemList;
    private Hashtable _itemSprites = new Hashtable();
    private int _maxID = 0;

    public ItemList()
    {
        // Load json file from Resources.
        TextAsset text_file = Resources.Load(ITEM_LIST) as TextAsset;
        if (text_file == null)
            Debug.LogError("\"item_list.json\" not found in Resources folder.");
        ParseToHashTable(text_file.text);
    }

    // Save id and sprite to hash table.
    private void ParseToHashTable(string json)
    {
        _itemList = JSON.Parse(json);
        for (int i = 0; i < _itemList.Count; i++)
        {
            int id = _itemList[i]["id"].AsInt;
            if (id > _maxID)
                _maxID = id;
            Sprite sprite = Resources.Load(_itemList[i]["path"]) as Sprite;
            _itemSprites.Add(id, sprite);
        }
    }

    // Get sprite by id.
    public Sprite GetSprite(int id)
    {
        return _itemSprites[id] as Sprite;
    }

    public int MaxID
    {
        get { return _maxID; }
    }
}

