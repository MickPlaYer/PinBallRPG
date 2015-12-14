using UnityEngine;
using System.Collections;

public class BossMagicArrow : MonoBehaviour
{

    public Shoot _shoot;
    public float _cd = 0.5f;
    bool _trigger = false;
    public int _extraDamage = 0;
    // Use this for initialization
    void Start()
    {
        _trigger = false;
        _extraDamage = PlayerPrefs.GetInt("current_level");

    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Ball" && _trigger)
        {
            _cd -= Time.deltaTime;
            if (_cd <= 0)
            {
                _shoot.Cast(5 + _extraDamage);
                _cd = 1f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("enter");
            _trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("exit");
            _trigger = false;
            _cd = 1f;
        }
    }
}
