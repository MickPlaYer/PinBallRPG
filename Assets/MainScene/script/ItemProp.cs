using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemProp : MonoBehaviour {
    public int _itemIndex;
    public Text _amount;
    int _itemAmount,_state;
    public GameObject _selectFrame;
    public bool _onSelected = false;
	// Use this for initialization
	void Start () {
        //_selectFrame =Instantiate( GameObject.FindGameObjectWithTag("selectFrame"),transform.position,transform.rotation) as GameObject;

    }
	
	// Update is called once per frame
	void Update () {
       // if(_onSelected)
            //_selectFrame.transform.position = transform.position;

    }

    public void Set(int index,int amount,Sprite image,int state)
    {
        _selectFrame.SetActive(false);
        Start();
        _state = state;
        _itemIndex = index;
        _itemAmount = amount;
        if (amount != 0)
            _amount.text = _itemAmount.ToString();
        else
            _amount.text = "";
        gameObject.transform.GetComponent<Image>().sprite = image;

    }

    public void OnItemPressed()
    {
        if (_state == 1)//Inventory
        {
            transform.parent.GetComponent<InvetoryController>().OnItemPressed(_itemIndex);
            transform.parent.GetComponent<InvetoryController>()._buttonText.text = "裝備";
            transform.parent.GetComponent<InvetoryController>()._EB._state = 0;
        }
        else if (_state == 2)//Shop
        {
            transform.parent.GetComponent<ShopItem>().OnItemPressed(_itemIndex);
        }
        else if (_state == 3)//equipment
        {
            transform.parent.GetComponent<ShowEquipItem>().OnItemPressed(_itemIndex);
            transform.parent.GetComponent<ShowEquipItem>()._buttonText.text = "卸下";
            transform.parent.GetComponent<ShowEquipItem>()._EB._state = 1;
        }
        else if (_state == 4)//forge
        {
     
            transform.parent.GetComponent<ForgeSystem>().SetIndex(_itemIndex);

        }
        setSelectFrameDefault();
        _onSelected = true;
        _selectFrame.SetActive(true);
    }

    public void setSelectFrameDefault()
    {
       
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("item"))
        {
            GO.GetComponent<ItemProp>()._onSelected = false;
            GO.GetComponent<ItemProp>()._selectFrame.SetActive(false);
        }
       
    }
}
