using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CleanData : MonoBehaviour {
    public HeroData3 _heroData;
    public GameObject _askPanel,_afterAsk;
	// Use this for initialization
	void Start () {
        _askPanel.SetActive(false);
        _afterAsk.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doCleanData()
    {
        _heroData.Initialize();
        _askPanel.SetActive(false);
        _afterAsk.SetActive(true);
    }

    public void askIf()
    {
        _askPanel.SetActive(true);
    }

    public void setPanelFalse()
    {
        _askPanel.SetActive(false);
    }
    public void closeAfter()
    {
        _afterAsk.SetActive(false);
    }

}
