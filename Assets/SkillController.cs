using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SkillController : MonoBehaviour {
    public MagicArrow _magicArrow;
    string _equipment;
	// Use this for initialization
	void Start () {
        _equipment = PlayerPrefs.GetString("_equipment");

        _magicArrow.gameObject.SetActive(false);
        SetSkill();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSkill()
    {
        if (Check(5)) _magicArrow.gameObject.SetActive(true);
    }

    public bool Check(int id)
    {
        var equipment = JSON.Parse(_equipment);
        for(int i=0;i<equipment.Count;i++)
        {
            if(equipment[i]["id"].AsInt == id)
            {
                return true;
            }
        }

        return false;
    }
}
