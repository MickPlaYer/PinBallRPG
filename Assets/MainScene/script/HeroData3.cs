using UnityEngine;
using System.Collections;
using SimpleJSON;

public class HeroData3 : MonoBehaviour {
    private const string DEFAULT_HERO = "default_hero_data";
    string _item_list;
    string _item_box;
    string _hero_data;
    string _equipment;
    string _battleValue;
    public int _bAtk = 0, _bHp = 0, _bRe = 0;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
        ReLoad();

        var box = JSON.Parse(_item_box);
        var list = JSON.Parse(_item_list);
        var data = JSON.Parse(_hero_data);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public string getBox()
    {
        return _item_box;
    }

    public string getList()
    {
        return _item_list;
    }

    public string getData()
    {
        return _hero_data;
    }

    public string getEqu()
    {
        return _equipment;
    }

    public string getBattleValue()
    {
        return _battleValue;
    }

    public void setBox()
    {
        _item_box = _item_box + "";
    }

    public void setEqu(int id,int amount)
    {
        bool hasItem = false;
        var box = JSON.Parse(_item_box);
        var equ = JSON.Parse(_equipment);
        if (equ.Count != 0)
        {
            for (int i = 0; i < equ.Count; i++)
            {
                if (equ[i]["id"].AsInt == id)
                {
                    equ[i]["amount"] = (equ[i]["amount"].AsInt + amount).ToString();
                    hasItem = true;
                }
            }

           
        }
        if (!hasItem)
        {
            string itemToAdd = "{  \"id\"  " + ":" + id + ", \"amount\" " + ":" + amount + "}";
            equ.Add(JSON.Parse(itemToAdd));
        }

        _equipment = equ.ToString();

        for (int i = 0; i < box.Count; i++)
        {
            if (box[i]["id"].AsInt == id)
            {
                box.Remove(i);
                // Debug.Log("box:" +_item_box);
            }
        }

        _item_box = box.ToString();
        PlayerPrefs.SetString("item_box", _item_box);
        PlayerPrefs.SetString("_equipment", _equipment);
        setValue();
        //ReLoad();
        // Debug.Log(_equipment);

    }

    public void setTakeOff(int id, int amount)
    {
        bool hasItem = false;
        var equ = JSON.Parse(_equipment);
        var box = JSON.Parse(_item_box);
        if (box.Count != 0)
        {
            for (int i = 0; i < box.Count; i++)
            {
                if (box[i]["id"].AsInt == id)
                {
                    box[i]["amount"] = (box[i]["amount"].AsInt + amount).ToString();
                    hasItem = true;
                }
            }

        }

        if (!hasItem)
        {
            string itemToAdd = "{  \"id\"  " + ":" + id + ", \"amount\" " + ":" + amount + "}";
            box.Add(JSON.Parse(itemToAdd));
        }

        _item_box = box.ToString();

        for (int i = 0; i < equ.Count; i++)
        {
            if (equ[i]["id"].AsInt == id)
            {
                equ.Remove(i);
                Debug.Log("box:" +_item_box);
            }
        }
       _equipment = equ.ToString();
        setValue();
        //ReLoad();
        PlayerPrefs.SetString("item_box", _item_box);
        PlayerPrefs.SetString("_equipment", _equipment);
         Debug.Log(_equipment);
    }

    public void setValue()
    {

         _bAtk =  _bHp =  _bRe = 0;
        var equ = JSON.Parse(_equipment);
        var list = JSON.Parse(_item_list);
        var data = JSON.Parse(_hero_data);
        var battleValue = JSON.Parse(_battleValue);
        if(equ.Count!=0)
        {
            for (int i = 0; i < equ.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (equ[i]["id"].AsInt == list[j]["id"].AsInt)
                    {

                        _bAtk += (list[j]["attack_point"].AsInt * equ[i]["amount"].AsInt);
                        _bHp += (list[j]["hit_point"].AsInt * equ[i]["amount"].AsInt);
                        _bRe += (list[j]["recovery"].AsInt * equ[i]["amount"].AsInt);
                    }
                }
            }

        }

        battleValue["hit_point"] = (data["hit_point"].AsInt + _bHp).ToString();

        battleValue["attack_point"] = (data["attack_point"].AsInt + _bAtk).ToString();
        battleValue["recovery"] = (data["recovery"].AsInt + _bRe).ToString();
        _battleValue = battleValue.ToString();
        PlayerPrefs.SetString("battle_value", _battleValue);
        Debug.Log(battleValue);
    }


    public void BuyItem(int id, int amount)
    {
        bool hasItem = false;

        var box = JSON.Parse(_item_box);
        for (int i = 0; i < box.Count; i++)
        {
            if (box[i]["id"].AsInt == id)
            {
                box[i]["amount"] = (box[i]["amount"].AsInt + amount).ToString();
                hasItem = true;
            }
        }

        if (!hasItem)
        {
            string itemToAdd = "{  \"id\"  " + ":" + id + ", \"amount\" " + ":" + amount + "}";
            box.Add(JSON.Parse(itemToAdd));
        }
        _item_box = box.ToString();
        //ReLoad();
        // Debug.Log(_equipment);
    }

    public void ReLoad()
    {
        string default_box = Resources.Load<TextAsset>("default_item_box").text;
        string default_list = Resources.Load<TextAsset>("item_list").text;
        string default_hero_data = Resources.Load<TextAsset>("default_hero_data").text;
        string default_equipment = Resources.Load<TextAsset>("equipment").text;
        
        // Get string from PlayerPrefs.
            _battleValue = PlayerPrefs.GetString("battle_value", default_hero_data);
        _item_box = PlayerPrefs.GetString("item_box", default_box);

        _hero_data = PlayerPrefs.GetString("_hero_data", default_hero_data);

        _item_list = PlayerPrefs.GetString("_item_list", default_list);
        _equipment = PlayerPrefs.GetString("_equipment", default_equipment);
    }

    public void Initialize()
    {
        Debug.Log("done");
        string default_box = Resources.Load<TextAsset>("default_item_box").text;
        string default_list = Resources.Load<TextAsset>("item_list").text;
        string default_hero_data = Resources.Load<TextAsset>("default_hero_data").text;
        string default_equipment = Resources.Load<TextAsset>("equipment").text;
        PlayerPrefs.SetString("item_box", default_box);
        PlayerPrefs.SetString("_hero_data", default_hero_data);
        PlayerPrefs.SetString("_item_list", default_list);
        PlayerPrefs.SetString("_equipment", default_equipment);
         PlayerPrefs.SetString("battle_value", default_hero_data);
        ReLoad();
    }
}
