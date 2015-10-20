using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardPad : MonoBehaviour
{
    public Image[] _items;
    private int _index = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Add item image to show.
    public void AddItemImage(Sprite sprite)
    {
        gameObject.SetActive(true);
        _items[_index].sprite = sprite;
        _items[_index].gameObject.SetActive(true);
        _index++;
    }
}
