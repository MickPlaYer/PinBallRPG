using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CDController : MonoBehaviour{
    public float _CD = 0;
    public float _coldTime = 0;
    public bool _trigger = false;
    public GameObject _mask;
   public  Text _cdText;
  

    // Use this for initialization
    void Start()
    {
        _mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_trigger)
        {
            _coldTime += Time.deltaTime;
            _cdText.text = (_CD - _coldTime).ToString("0.00");
            if (_coldTime >= _CD)
            {
                _mask.SetActive(false);
                _trigger = false;
                _coldTime = 0;
            }
        }
    }

    public void SetTrigger(bool trigger)
    {
        _mask.SetActive(true);
        _trigger = trigger;
    }

    public void SetCD(float cd)
    {
        _CD = cd;
    }

    public void SetColdTime(float coldTime)
    {
        _coldTime = coldTime;
    }

  

}
