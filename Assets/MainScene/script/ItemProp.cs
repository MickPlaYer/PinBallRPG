using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemProp : MonoBehaviour {
    public int _itemIndex;
    public Text _amount;
    int _itemAmount,_state;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

       
	}

    public void Set(int index,int amount,Sprite image,int state)
    {
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
    }
}
