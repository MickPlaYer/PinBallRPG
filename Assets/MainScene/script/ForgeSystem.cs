using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
public class ForgeSystem : MonoBehaviour {
    public HeroData3 _heroData;
    public Image _target, _material1, _material2, _material3;
    string _list,_item_box;
    public Slider _slider;
    public GameObject _pref;
    int _selectedIndex=0, _maxAmountToMake=0;
	// Use this for initialization
    void Awake()
    {
        _list = PlayerPrefs.GetString("item_list", Resources.Load<TextAsset>("item_list").text);
        _item_box = PlayerPrefs.GetString("item_box", Resources.Load<TextAsset>("default_item_box").text);
      
    }
	void Start () {
        ShowItem();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Manufacture()
    {
        var list = JSON.Parse(_list);
        if (Manufacturable())
        {
            for(int i =0;i<list[_selectedIndex]["material"].Count;i++)
            {
                _heroData.takeFromBox(list[_selectedIndex]["material"][i]["id"].AsInt, (int)_slider.value* list[_selectedIndex]["material"][i]["amount"].AsInt);
            }

            _heroData.putInBox(list[_selectedIndex]["id"].AsInt, _maxAmountToMake);
        }
    }

    public bool Manufacturable()
    {
        var list = JSON.Parse(_list);
        if (list[_selectedIndex]["manufacturable"].AsInt == 1)
            return true;
        else
            return false;
    }

    public void SetIndex(int id)
    {
        var list = JSON.Parse(_list);

    _selectedIndex = id;

        if(Manufacturable())
        {        
         SetSliderMax();
        }
        else
        {
            _slider.maxValue = 0;
            _maxAmountToMake = 0;
        }
        _slider.value = 0;
    }

    public void SetSliderMax()
    {
        _slider.maxValue = _maxAmountToMake=Calculate();
    }

    public int Calculate()
    {
        var list = JSON.Parse(_list);
        var box = JSON.Parse(_item_box);
        int maxToMake = 99;
        if(list[_selectedIndex]["material"].Count>0)  ////////////calculate the number of item you can make
        for(int i=0;i<list[_selectedIndex]["material"].Count;i++)
            {
                int amount = 0;
                if (box.Count>0)
                for(int j =0;j<box.Count;j++)
                {
                    if(box[j]["id"].AsInt== list[_selectedIndex]["material"][i]["id"].AsInt)
                    {
                        if (box[j]["amount"].AsInt > list[_selectedIndex]["material"][i]["amount"].AsInt)
                            amount = box[j]["amount"].AsInt / list[_selectedIndex]["material"][i]["amount"].AsInt;
                        else
                            amount = 0;           
                    }
                }
                if(amount<maxToMake)
                {
                    maxToMake = amount;
                }
            }
        return maxToMake;
    }

    public void ShowItem()
    {
        Initialize();
        var _item_list = JSON.Parse(_list);

        
            for (int i = 1; i < _item_list.Count; i++)
            {
                if (_item_list[i]["manufacturable"].AsInt == 1)
                {
                //Debug.Log("item:" + _item_list[i]["name"] + "\n" + _item_box[j]["amount"]);
                GameObject item = (GameObject)Instantiate(_pref, Vector3.zero, Quaternion.identity);
                    item.GetComponent<ItemProp>().Set(i,0, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite, 4);
                    item.transform.parent = gameObject.transform;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }

            }
        PanelHeightController();
    }
    public void Initialize()
    {
        if(transform.childCount!=0)
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void PanelHeightController()
    {
        var list = JSON.Parse(_list);
        int length = list.Count-1;
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

    public void ShowMaterial()
    {

    }

}
