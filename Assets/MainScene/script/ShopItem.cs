using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class ShopItem : MonoBehaviour {

    public Slider _amountSlider;
    public Text _amountText, _totalPriceText;
    int _amount = 1, _price;

    public HeroData3 _heroData;
    public GameObject _itemPref;
    public GameObject _parent;
    public CanvasFixed _canvasFixed;
    public Text _itemInfoText;
    public Text _itemPriceText;
    public int _selectingId;

    TradeSystem _TS;

    JSONNode _heroInfo;
    JSONNode _item_list;
    JSONNode _item_box;


    int _itemSortQuan = 0; //物品種類數量
    GameObject[] _items = new GameObject[4];
    // Use this for initialization
    void Start()
    {
        ShowItem();
    }

    void Update()
    {
        _amountSlider.maxValue = _heroInfo["money"].AsInt / _price;
    }
    void PanelHeightController()
    {
        int length = _itemSortQuan;
        if (length % 2 != 0)
        {
            length += 1;
            length /= 2;
        }
        else
        {
            length /= 2;
        }
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, length * 100);
    }

    public void ShowItem()
    {
        Initialize();
        _item_box = JSON.Parse(_heroData.getBox());
        _item_list = JSON.Parse(_heroData.getList());
        _heroInfo = JSON.Parse(_heroData.getData());
        _itemSortQuan = _item_box.Count;

            for (int i = 1; i < _item_list.Count; i++)
            {
                    GameObject item = (GameObject)Instantiate(_itemPref, Vector3.zero, Quaternion.identity);
                    item.GetComponent<ItemProp>().Set(i, 0, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite,2);
                    item.transform.parent = gameObject.transform;
                    item.transform.localScale = new Vector3(1, 1, 1);

        }
        PanelHeightController();
    }



    public void OnItemPressed(int index)
    {
        _itemInfoText.text = _item_list[index]["description"].ToString();
        _itemPriceText.text = "Price : "+_item_list[index]["price"].ToString();
        _selectingId = index;
    }

    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Buy(int index,int amount)
    {
        _TS.costMoney(_selectingId, amount, 1);
    
    }


}
