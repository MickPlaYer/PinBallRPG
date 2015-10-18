using UnityEngine;
using System.Collections;

public class CDController : MonoBehaviour{
    public float _CD = 0;
    public float _coldTime = 0;
    public bool _trigger = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_trigger)
        {
            _coldTime += Time.deltaTime;
            if (_coldTime >= _CD)
            {
                _trigger = false;
                _coldTime = 0;
            }
        }
    }

    public void SetTrigger(bool trigger)
    {
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
