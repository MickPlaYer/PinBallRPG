using UnityEngine;
using System.Collections;

public class ComboCancle : MonoBehaviour
{
    private ComboBox _comboBox;

    // Use this for initialization
    void Start()
    {
        _comboBox = GameObject.Find("ComboShow").GetComponent<ComboBox>(); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Cancle combo count if hit by ball.
        if (collision.gameObject.name == "Ball")
        {
            _comboBox.CancleCombo();
        }
    }
}
