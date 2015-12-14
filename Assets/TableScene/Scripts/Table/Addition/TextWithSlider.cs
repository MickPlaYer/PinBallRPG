using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextWithSlider : MonoBehaviour {
    public Text _level;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _level.text = GetComponent<Slider>().value.ToString();
    }

    public void add()
    {
        GetComponent<Slider>().value += 1;
    }
    public void reduce()
    {
        GetComponent<Slider>().value -= 1;
    }
}
