using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {
    public Slider _levelSelect;

    public HeroData3 _heroData;
    float _bAtk=0, _bHp=0, _bRe=0;
    string hero;
    // Use this for initialization
    public void Start () {
        _levelSelect.maxValue = _heroData.getMaxLevel();
        _levelSelect.value = _levelSelect.maxValue;
         hero = PlayerPrefs.GetString("battle_value", _heroData.getData());
       
        setValue();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void setValue()
    {
        PlayerPrefs.SetInt("current_level", (int)_levelSelect.value);
        _heroData.setValue();    
    }

    public void initialSlider()
    {
        _levelSelect.value = 1;
    }
}
