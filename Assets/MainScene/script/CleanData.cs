using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CleanData : MonoBehaviour {
    public HeroData3 _heroData;
    public GameObject _panel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doCleanData()
    {
        _heroData.Initialize();
    }

    public void askIf()
    {
        _panel.SetActive(true);
    }

    public void setPanelFalse()
    {
        _panel.SetActive(false);
    }


}
