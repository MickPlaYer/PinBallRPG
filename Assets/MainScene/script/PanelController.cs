using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetPanelDefalt(GameObject panel)
    {
        panel.transform.position=new Vector2(0,0);
        panel.SetActive(false);
    }
}
