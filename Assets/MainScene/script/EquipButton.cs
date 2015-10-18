using UnityEngine;
using System.Collections;
using SimpleJSON;

public class EquipButton : MonoBehaviour {
    public int _state=0;
    public InvetoryController _IC;
    public ShowEquipItem _SE;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPress()
    {
        switch(_state)
        {
            case 0:
               
                _IC.Equip();
                break;
            case 1:
                _SE.TakeOff();
                break;
        }
    }

}
