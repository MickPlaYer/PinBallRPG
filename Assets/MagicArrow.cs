using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MagicArrow : MonoBehaviour {
    public Shoot _shoot;
    public float _cd=0.5f;
    bool _trigger =false;
    public int _extraDamage = 0,_staticDamage=5,_coif=1;
	// Use this for initialization
	void Start () {
        _extraDamage =Mathf.RoundToInt( JSON.Parse(PlayerPrefs.GetString("battle_value"))["attack_point"].AsInt*0.01f)*_coif;
        Debug.Log(_extraDamage);
	}
	
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D other)    {
	    
        if(other.gameObject.tag=="Boss"&& _trigger)
        {
            _cd -= Time.deltaTime;
            if(_cd<=0)
            {
                _shoot.Cast(_extraDamage + _staticDamage);
                _cd = 0.5f;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag=="Boss")
        {
            Debug.Log("enter");
            _trigger = true;
        }
   }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("exit");
            _trigger = false;
            _cd = 0.5f;
        }
    }
}
