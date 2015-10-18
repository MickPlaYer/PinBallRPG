using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class ShowHeroInfo : MonoBehaviour {
    bool _trigger = false;
    public HeroData3 _heroData;
    string _battleValue;
    public Text[] _texts;
	// Use this for initialization
	void Start () {
        //_heroData = GameObject.FindGameObjectWithTag("HeroData").GetComponent<HeroData3>();
        _battleValue = PlayerPrefs.GetString("battle_value", _heroData.getData());
        ShowData();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ShowData()
    {
        if(_trigger)
        {
            _heroData.setValue();
            int i = 0;
            var battleValue = JSON.Parse(_heroData.getBattleValue());
            foreach (Text t in _texts)
        {
            
           
            t.text = battleValue[i];
            i++;
        }
        }
        
    }
    public void Trigger()
    {
       
        if (!_trigger)
        {
            gameObject.SetActive(true);
            _trigger = true;
            ShowData();
        }
        else
        {
            gameObject.SetActive(false);
            _trigger = false;
        }
    }


}
