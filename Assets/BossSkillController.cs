using UnityEngine;
using System.Collections;

public class BossSkillController : MonoBehaviour
{
    public BossMagicArrow _BMA;
    public int _currentLevel;
    // Use this for initialization
    void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("current_level");
        _BMA.gameObject.SetActive(false);
        SetSkill();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSkill()
    {
        if (_currentLevel % 5 == 0)
        {
            _BMA.gameObject.SetActive(true);
        }
    }
}
