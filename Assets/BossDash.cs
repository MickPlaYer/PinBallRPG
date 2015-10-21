using UnityEngine;
using System.Collections;

public class BossDash : MonoBehaviour {
    public GameObject _boss;
    public AudioSource _audio;
    bool _moveTrigger = false;
    Vector3 _targetPos;
    float _CD;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(_moveTrigger)
        {
            _boss.transform.position = Vector3.Lerp(_boss.transform.position, _targetPos, 0.1f);
        }
	}

    public void Cast(Vector2 direction)
    {
        _audio.Play();
        _moveTrigger = true;
        _targetPos = _boss.transform.position + new Vector3(0,5,0);
    }
}
