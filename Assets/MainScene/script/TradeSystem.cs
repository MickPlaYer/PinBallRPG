using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour {
    public HeroData3 _heroData;


    JSONNode _heroInfo;
    JSONNode _item_list;
    JSONNode _item_box;
    JSONNode _equipment;
    int _selectingItemId=0;
    // Use this for initialization
    void Start () {
        //_heroData = (GameObject.FindGameObjectWithTag("HeroData") as GameObject).GetComponent<HeroData3>();
        _item_box = JSON.Parse(_heroData.getBox());
        _item_list = JSON.Parse(_heroData.getList());
        _heroInfo = JSON.Parse(_heroData.getData());
        _equipment = JSON.Parse(_heroData.getEqu());
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SetValue()
    {

    }
    //increase or decrease the number of item's amount
    public void ChangeAmount(int id,int amount)
    {
        for(int i = 0;i <_item_box.Count;i++)
        {
            if( _item_box[i]["id"].AsInt==id)
            {
                _item_box[i]["amount"] += amount;
            }
        }        
    }

    public void costMoney(int id ,int amount, float discount)
    {
        int price=0;
        for (int i = 0; i < _item_list.Count; i++)
        {
            if (_item_box[i]["id"].AsInt == id)
            {
                price = _item_box[i]["price"].AsInt;
            }
        }
        price = Mathf.RoundToInt(price * discount);
        if(_heroInfo["money"].AsInt>=price)
        {
            _heroInfo["money"] = (_heroInfo["money"].AsInt - price).ToString() ;
        }
    }

  
}
