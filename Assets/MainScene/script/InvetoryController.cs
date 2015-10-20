using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;


public class InvetoryController : MonoBehaviour {
    public HeroData3 _heroData;
    public GameObject _itemPref;
    public GameObject _parent;
    public CanvasFixed _canvasFixed;
    public Text _itemInfoText,_itemName;
    public ShowEquipItem _SEI;
    int _selectingItemId = 0,_amount=0;

    public Text _buttonText;
    public EquipButton _EB;

    JSONNode _heroInfo;
    JSONNode _item_list;
    JSONNode _item_box;


    int _itemSortQuan = 0; //物品種類數量
    GameObject[] _items = new GameObject[4];
	// Use this for initialization
	void Start () {

     
  
    }
	
	// Update is called once per frame
	void Update () {

    }

    void LoadJsonToString()
    {
        _item_list = Resources.Load<TextAsset>("item_list").text;
    }

    void PanelHeightController()
    {
        int length =_itemSortQuan ;
        if(length%2!=0)
        {
            length += 1;
            length /= 2;
        }
        else
        {
            length /= 2;
        }
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, length*100);
    }


    public void ShowItem()
    {
        Initialize();
        _item_box = JSON.Parse(_heroData.getBox());
        _item_list = JSON.Parse(_heroData.getList());
        _heroInfo = JSON.Parse(_heroData.getData());
        _itemSortQuan = _item_box.Count;
        for (int j = 0; j < _item_box.Count; j++)
        {

            for (int i = 0; i < _item_list.Count; i++)
            {
                if (_item_list[i]["id"].AsInt == _item_box[j]["id"].AsInt)
                {
                    if (_item_box[j]["amount"].AsInt != 0)
                    {
                        //Debug.Log("item:" + _item_list[i]["name"] + "\n" + _item_box[j]["amount"]);
                        GameObject item = (GameObject)Instantiate(_itemPref, Vector3.zero, Quaternion.identity);
                        item.GetComponent<ItemProp>().Set(i, _item_box[j]["amount"].AsInt, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite, 1);
                        item.transform.SetParent( gameObject.transform);
                        item.transform.localScale = new Vector3(1, 1, 1);
                    }
                    
                }

            }
        }
        PanelHeightController();
    }



public void OnItemPressed(int index)
    {
        _itemName.text= _item_list[index]["name"].ToString().Replace("\"","");
        _itemInfoText.text = _item_list[index]["description"].ToString().Replace("\"", "");
        _selectingItemId = index;

        for(int i =0;i<_item_box.Count;i++)
        {
            if(_item_box[i]["id"].AsInt==_selectingItemId)
            {
                _amount = _item_box[i]["amount"].AsInt;
            }
        }
    }

    public void Initialize()
    {
        for(int i =0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Equip()
    {
        //Debug.Log("BeforeEquip");
        if (_selectingItemId != 0)
        {
            _itemName.text = "";
            _itemInfoText.text = "";
            _heroData.setEqu(_selectingItemId, _amount);
        }
        ShowItem();
        _SEI.ShowItem();
        _selectingItemId = 0;
        //Debug.Log("equip");
    }


}
