using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

public class HeroData
{
    private const string DEFAULT_HERO = "default_hero_data";
    private float _maxHitPoint;
    private float _hitPoint;
    private float _attack;
    private float _recover;
    private HPBar _hpBar;

    public HeroData()
    {
        LoadBattleValue();
        _hpBar = GameObject.Find("HeroHPBar").GetComponent<HPBar>();
    }

    // Load hero battle value from PlayerPrefs.
    private void LoadBattleValue()
    {
        TextAsset default_hero = Resources.Load(DEFAULT_HERO) as TextAsset;
        string hero = PlayerPrefs.GetString("battle_value", default_hero.text);
        var hero_data = JSON.Parse(hero);
        _hitPoint = _maxHitPoint = hero_data["hit_point"].AsFloat;
        _attack = hero_data["attack_point"].AsFloat;
        _recover = hero_data["recovery"].AsFloat;
    }

    public float MaxHP
    {
        get { return _maxHitPoint; }
        set { _maxHitPoint = value; }
    }

    public float HP
    {
        get { return _hitPoint; }
        set { SetHitPoint(value); }
    }

    public float ATK
    {
        get { return _attack; }
        set { _attack = value; }
    }

    public float REC
    {
        get { return _recover; }
        set { _recover = value; }
    }
    
    // Set hitpoint and follow its rule.
    private void SetHitPoint(float value)
    {
        if (value > _maxHitPoint)
            _hitPoint = _maxHitPoint;
        else
            _hitPoint = value;
        _hpBar.Scale = _hitPoint / _maxHitPoint;
    }
}