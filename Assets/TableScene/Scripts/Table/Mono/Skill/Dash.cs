using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public AudioSource _audio;
    public GameObject _leftMask,_rightMask;
    bool _leftTrigger=false, _rightTrigger=false;
    float _leftCD, _rightCD,_DL,_DR;
    Rect _LC, _RC;
    

    // Cast the skill.
    void Start()
    {
        _leftTrigger = false;
        _rightTrigger = false;
        _rightMask.gameObject.SetActive(false);
        _leftMask.gameObject.SetActive(false);
        _LC = _leftMask.gameObject.GetComponent<RectTransform>().rect;
        _RC = _rightMask.gameObject.GetComponent<RectTransform>().rect;
        _DL = _LC.width;
        _DR = _RC.height;
        _leftCD = 3;
        _rightCD = 3;
    }
        void Update()
    {
        if(_leftTrigger)
        {
            _leftCD -= Time.deltaTime;
            //_LC.width -= _DL /3 * Time.deltaTime;
            _leftMask.transform.localScale=new Vector3(_leftMask.transform.localScale.x-Time.deltaTime / 3,1,1);
            if (_leftCD<=0)
            {
                _leftMask.gameObject.SetActive(false);
                _leftTrigger = false;
                _leftMask.transform.localScale = new Vector3(1, 1, 1);
               _leftCD = 3;
                _LC.width = _DL;
            }
        }
        if (_rightTrigger)
        {
            _rightCD -= Time.deltaTime;
            _RC.height -= _DR / 3 * Time.deltaTime;
            _rightMask.transform.localScale = new Vector3(1, _rightMask.transform.localScale.y- Time.deltaTime / 3, 1);
            if (_rightCD <=0)
            {
                _rightMask.gameObject.SetActive(false);
                _rightTrigger = false;
                _rightMask.transform.localScale = new Vector3(1, 1, 1);
                _rightCD = 3;
                _RC.height = _DR;
            }
        }
    }
    public void Cast(Vector2 direction)
    {
        _audio.Play();
        _rigidbody.AddForce(direction * 3000);
    }

    public void left()
    {
        if(!_leftTrigger)
        {
            _leftMask.gameObject.SetActive(true);
            Cast(Vector2.left);
            _leftTrigger = true;
        }

    }

    public void right()
    {
        if(!_rightTrigger)
        {
            _rightMask.gameObject.SetActive(true);
            Cast(Vector2.right);
            _rightTrigger = true;
        }

    }
}
