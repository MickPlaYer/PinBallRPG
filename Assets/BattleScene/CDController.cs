using UnityEngine;
using System.Collections;

public class CDController : MonoBehaviour{
    public float _CD = 0;
    public float _coldTime = 0;
    public bool _trigger = false;
    GameObject _mask;
    Vector3 _defaultMaskPos;

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
            _mask.transform.position =new Vector3(_mask.transform.position.x, _mask.transform.position.y - _CD * 45 * Time.deltaTime, _mask.transform.position.z);
            if (_coldTime >= _CD)
            {
                _mask.transform.position = _defaultMaskPos;
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

    public void SetMask(GameObject mask)
    {
        _mask = mask;
        _defaultMaskPos = _mask.transform.position;
    }

}
