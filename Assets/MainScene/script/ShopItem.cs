using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class ShopItem : MonoBehaviour {

    public Slider _amountSlider;
    public Text _amountText, _totalPriceText,_money,_singlePrice;
    int _amount = 0, _price = 1, _totalPrice;

    public HeroData3 _heroData;
    public GameObject _itemPref;
    public GameObject _parent;
    public CanvasFixed _canvasFixed;
    public Text _itemInfoText;
    public int _selectingId;

    public Image _itemToBuy;

    //TradeSystem _TS;

    //JSONNode _heroInfo;
    JSONNode _item_list;


  
   // GameObject[] _items = new GameObject[4];
    // Use this for initialization
    void Start()
    {
        _item_list = JSON.Parse( Resources.Load<TextAsset>("item_list").text);
        Initialize();
        _itemToBuy.gameObject.SetActive(false);
        ShowItem();
        _amountSlider.maxValue = 0;
    }

    void Update()
    {
        _amount = (int)_amountSlider.value;
       // _amountText.text = "數量: "+_amount.ToString();
        _totalPrice = _amount * _price;
        _totalPriceText.text = _totalPrice.ToString();
        _money.text ="金錢: " +JSON.Parse(_heroData.getData())["money"].AsInt;
    }
    void PanelHeightController()
    {
        int length = JSON.Parse(_heroData.getList()).Count-1;
        Debug.Log(length);
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

            for (int i = 1; i < _item_list.Count; i++)
            {
                    GameObject item = (GameObject)Instantiate(_itemPref, Vector3.zero, Quaternion.identity);
                    item.GetComponent<ItemProp>().Set(i, 0, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite,2);
                    item.transform.SetParent(gameObject.transform) ;
                    item.transform.localScale = new Vector3(1, 1, 1);

        }
        PanelHeightController();
    }



    public void OnItemPressed(int index)
    {
        _itemInfoText.text = _item_list[index]["description"].ToString().Replace("\"", "");
        _totalPriceText.text = "Price : "+_item_list[index]["price"].ToString().Replace("\"", "");
        _selectingId = index;
         _price = _item_list[index]["price"].AsInt;
        _singlePrice.text = _price.ToString();
        _amountSlider.maxValue = JSON.Parse(_heroData.getData())["money"].AsInt / _price;
        _itemToBuy.sprite = Resources.Load<Sprite>(_item_list[index]["path"]) as Sprite;
        _itemToBuy.gameObject.SetActive(true);
        _amountSlider.value = 0;
       
    }

    public void Initialize()
    {
        _item_list = JSON.Parse(Resources.Load<TextAsset>("item_list").text);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        _itemToBuy.gameObject.SetActive(false);
        _amountSlider.maxValue = 0;
        _totalPriceText.text = "";
        _selectingId = 0;
        _itemInfoText.text = "";
        _singlePrice.text = "";
    }

    public void Buy()
    {
        _amount = (int)_amountSlider.value;
        if (_selectingId!=0)
        {
           // Debug.Log("buy");
            _heroData.BuyItem(_selectingId, _amount, _totalPrice);
        }


        _amountSlider.value = 0;
        _amount = (int)_amountSlider.value;
        //_amountText.text = "數量: " + _amount.ToString();
        _amountSlider.maxValue = JSON.Parse(_heroData.getData())["money"].AsInt / _price;
        Debug.Log("shop:" + JSON.Parse(_heroData.getData())["money"].AsInt);
        _totalPrice = _amount * _price;
        _totalPriceText.text = _totalPrice.ToString();
        _money.text = "金錢: " + JSON.Parse(_heroData.getData())["money"].AsInt;
    }


}
