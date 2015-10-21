using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardPad : MonoBehaviour
{
    public Image[] _items;
    private int _index = 0;

    // Add item image to show.
    public void AddItemImage(Sprite sprite)
    {
        gameObject.SetActive(true);
        _items[_index].sprite = sprite;
        _items[_index].gameObject.SetActive(true);
        _index++;
    }
}
