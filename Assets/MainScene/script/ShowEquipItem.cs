using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;


public class ShowEquipItem : MonoBehaviour {

    public HeroData3 _heroData;
    public GameObject _itemPref;
    public GameObject _parent;
    public CanvasFixed _canvasFixed;
    public Text _itemInfoText, _itemName;
    public InvetoryController _IC;
    int _selectingItemId = 0, _amount = 0;
    JSONNode _heroInfo;
    JSONNode _item_list;
    JSONNode _equipment;

    public Text _buttonText;
    public EquipButton _EB;

    int _itemSortQuan = 0; //物品種類數量
    GameObject[] _items = new GameObject[4];
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadJsonToString()
    {
        _item_list = Resources.Load<TextAsset>("item_list").text;
    }

    void PanelHeightController()
    {
        int length = _itemSortQuan ;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(length * 105,90);
    }


    public void ShowItem()
    {
        Initialize();
        _equipment = JSON.Parse(_heroData.getEqu());
        _item_list = JSON.Parse(_heroData.getList());
        _heroInfo = JSON.Parse(_heroData.getData());
        _itemSortQuan = _equipment.Count;
        for (int j = 0; j < _equipment.Count; j++)
        {
            for (int i = 0; i < _item_list.Count; i++)
            {
                if (_item_list[i]["id"].AsInt == _equipment[j]["id"].AsInt)
                {
                    //Debug.Log("item:" + _item_list[i]["name"] + "\n" + _item_box[j]["amount"]);
                    GameObject item = (GameObject)Instantiate(_itemPref, Vector3.zero, Quaternion.identity);
                    item.GetComponent<ItemProp>().Set(i, _equipment[j]["amount"].AsInt, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite, 3);
                    item.transform.SetParent( gameObject.transform);
                    item.transform.localScale = new Vector3(1, 1, 1);
                }

            }
        }
        PanelHeightController();
    }



    public void OnItemPressed(int index)
    {
        _itemName.text = _item_list[index]["name"].ToString().Replace("\"", "");
        _itemInfoText.text = _item_list[index]["description"].ToString().Replace("\"", "");
        _selectingItemId = index;

        for (int i = 0; i < _equipment.Count; i++)
        {
            if (_equipment[i]["id"].AsInt == _selectingItemId)
            {
                _amount = _equipment[i]["amount"].AsInt;
            }
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void TakeOff()
    {
        //Debug.Log("BeforeTakeOff");
        if(_selectingItemId!=0)
        {
            _itemName.text = "";
            _heroData.setTakeOff(_selectingItemId, _amount);
            _itemInfoText.text = "";
        }
     
        _IC.ShowItem();
        ShowItem();
        _selectingItemId = 0;
       // Debug.Log("Took Off");
    }
}
