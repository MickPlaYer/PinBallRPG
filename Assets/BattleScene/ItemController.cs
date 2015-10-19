using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ItemController : MonoBehaviour {
   // HeroData3 _heroData;
    int _index = 0;
    public GameObject[] _items;

    JSONNode _equipment;
    JSONNode _item_list;
    // Use this for initialization
    void Start () {
        //GameObject g = GameObject.FindGameObjectWithTag("HeroData") as GameObject;
        //_heroData = g.GetComponent<HeroData3>();
        _item_list = JSON.Parse(PlayerPrefs.GetString("_item_list", Resources.Load<TextAsset>("item_list").text));
        _equipment = JSON.Parse(PlayerPrefs.GetString("_equipment", Resources.Load<TextAsset>("equipment").text));
        Debug.Log("list:  " + _item_list.ToString());
        Set();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set()
    {
        _index = 0;
        if (_equipment.Count != 0)
        {
            for (int i = 0; i < _equipment.Count; i++)
            {
                for (int j = 0; j < _item_list.Count; j++)
                {
                    if (_equipment[i]["id"].AsInt == _item_list[j]["id"].AsInt)
                    {
                        Debug.Log("before: "+_index);
                        Debug.Log("before: " + _equipment[i]["id"].AsInt);
                        Debug.Log("before: " + _item_list[j]["path"]);
                        _items[_index].GetComponent<Items>().Set(_index, _equipment[i]["id"].AsInt, Resources.Load<Sprite>(_item_list[j]["path"]) as Sprite);
                        _items[_index].SetActive(true);
                        _index++;
                        Debug.Log(_index);
                      
                    }
                }
            }
        }
    }
}
