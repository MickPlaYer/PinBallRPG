using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
public class ForgeSystem : MonoBehaviour
{
    public HeroData3 _heroData;
    public Image[] _materia;
    public Text[] _Need;
    public Text[] _Own, _name;
    string _list;
    public Slider _slider;
    public GameObject _pref;
    int _selectedID = 0;
    // Use this for initialization
    void Awake()
    {
        _list = Resources.Load<TextAsset>("item_list").text;


    }
    void Start()
    {
        ShowItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Manufacture()
    {
        var list = JSON.Parse(_list);
        if (Manufacturable())
        {
            for (int i = 0; i < list[_selectedID]["material"].Count; i++)
            {
                _heroData.takeFromBox(list[_selectedID]["material"][i]["id"].AsInt, (int)_slider.value * list[_selectedID]["material"][i]["amount"].AsInt);
            }

            _heroData.putInBox(list[_selectedID]["id"].AsInt, (int)_slider.value);
        }

        _list = PlayerPrefs.GetString("item_list", Resources.Load<TextAsset>("item_list").text);
        ShowItem();
        ShowMaterial();
        SetSliderMax();
    }

    public bool Manufacturable()
    {
        var list = JSON.Parse(_list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]["id"].AsInt == _selectedID && list[i]["manufacturable"].AsInt == 1)
                return true;
        }
        return false;
    }

    public void SetIndex(int id)
    {
        //  var list = JSON.Parse(_list);

        _selectedID = id;

        if (Manufacturable())
        {
            SetSliderMax();
        }
        else
        {
            _slider.maxValue = 0;
        }
        _slider.value = 0;
        ShowMaterial();
    }

    public void SetSliderMax()
    {
        _slider.maxValue = Calculate();
    }

    public int Calculate()
    {
        var list = JSON.Parse(_list);
        var box = JSON.Parse(_heroData.getBox());
        int maxToMake = 99;
        if (list[_selectedID]["material"].Count > 0)  ////////////calculate the number of item you can make
            for (int i = 0; i < list[_selectedID]["material"].Count; i++)
            {
                int amount = 0;
                if (box.Count > 0)
                    for (int j = 0; j < box.Count; j++)
                    {
                        if (box[j]["id"].AsInt == list[_selectedID]["material"][i]["id"].AsInt)
                        {
                            if (box[j]["amount"].AsInt >= list[_selectedID]["material"][i]["amount"].AsInt)
                                amount = box[j]["amount"].AsInt / list[_selectedID]["material"][i]["amount"].AsInt;
                            else
                                amount = 0;
                        }
                    }
                if (amount < maxToMake)
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
                item.GetComponent<ItemProp>().Set(i, 0, Resources.Load<Sprite>(_item_list[i]["path"]) as Sprite, 4);
                item.transform.SetParent(gameObject.transform);
                item.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        PanelHeightController();
    }
    public void Initialize()
    {
        Awake();
        if (transform.childCount != 0)
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        _slider.value = 0;

        MaterialIni();
    }

    void PanelHeightController()
    {
        var list = JSON.Parse(_list);
        int length = list.Count - 1;
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
        MaterialIni();
        var list = JSON.Parse(_list);
        var box = JSON.Parse(_heroData.getBox());
        int materialInfo = 1,sprit=1;
        for (int x = 0; x < list.Count; x++)
        {
            if (list[x]["id"].AsInt == _selectedID)
            {
                _name[0].text = list[x]["name"].ToString().Replace("\"", "");
                _materia[0].sprite = Resources.Load<Sprite>(list[x]["path"]) as Sprite;
                _Need[0].text = ((int)_slider.value).ToString();

                for (int i = 0; i < list[x]["material"].Count; i++)
                {
                    int amount = (int)_slider.value;
                    if (amount > 0)
                        _Need[materialInfo].text = (list[x]["material"][i]["amount"].AsInt * amount).ToString();
                    else
                        _Need[materialInfo].text = (list[x]["material"][i]["amount"].AsInt).ToString();
                    materialInfo++;
                }
                materialInfo = 1;
                for (int i = 0; i < list[x]["material"].Count; i++)
                {
                    for (int z = 0; z < list.Count; z++)
                    {
                        if (list[x]["material"][i]["id"].AsInt == list[z]["id"].AsInt)
                        {
                            _name[sprit].text = list[z]["name"].ToString().Replace("\"", "");
                            _materia[sprit].sprite = Resources.Load<Sprite>(list[z]["path"]) as Sprite;
                            sprit++;
                            if (box.Count > 0)
                                for (int j = 0; j < box.Count; j++)
                                {
                                    if (box[j]["id"].AsInt == list[x]["id"].AsInt)
                                        _Own[0].text = (box[j]["amount"].AsInt).ToString();
                                    if (box[j]["id"].AsInt == list[z]["id"].AsInt)
                                    {
                                        _Own[materialInfo].text = (box[j]["amount"].AsInt).ToString();
                                        materialInfo++;
                                    }
                                }
                        }
                    }
                }
            }
        }


    }

    public void MaterialIni()
    {
        foreach (Image image in _materia)
        {
            image.sprite = Resources.Load<Sprite>("texture/ItemFrame") as Sprite;
        }
        foreach (Text T in _Need)
        {
            T.text = "";
        }
        foreach (Text T in _Own)
        {
            T.text = "";
        }
        foreach (Text T in _name)
        {
            T.text = "";
        }
    }

}
