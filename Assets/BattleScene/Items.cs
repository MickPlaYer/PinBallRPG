using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Items : MonoBehaviour {
    public ItemCast _IC;
    public int _itemIndex;
    public int _id;
    int _itemAmount;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     
    }

    public void Set(int index,int id, Sprite image)
    {
        _itemIndex = index;
        _id = id;
        _IC.SetAbilityCD(id, index);
        gameObject.transform.GetComponent<Image>().sprite = image;
    }

    public void OnPressed()
    {
        _IC.Cast(_id, _itemIndex);
    }
}
