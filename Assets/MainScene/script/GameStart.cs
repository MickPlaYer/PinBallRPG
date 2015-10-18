using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameStart : MonoBehaviour {
    public HeroData3 _heroData;
    float _bAtk=0, _bHp=0, _bRe=0;
    string hero;
    // Use this for initialization
    void Start () {

         hero = PlayerPrefs.GetString("battle_value", _heroData.getData());
        setValue();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setValue()
    {
        _heroData.setValue();    
    }

}
