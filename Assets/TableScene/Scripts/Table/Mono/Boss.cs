using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    const int LEVEL_RANGE = 3;
    private float _hitPoint = 50;
    private float _hitPointMax = 50;
    private float _atk = 10;
    private List<int> _itemIDs = new List<int>();
    private Color _color;
    public PinBallTable _table;
    public UILevelPad _levelPad;
    public SpriteRenderer _sprite;
    public SpriteRenderer _ring;
    public SpriteRenderer _back;

    // Use this for initialization
    void Start()
    {
        int gameLevel = PlayerPrefs.GetInt("current_level", 0);
        Random.seed = gameLevel;
        _color = new Color(Random.value, Random.value, Random.value);
        _sprite.color = _color;
        _ring.color = _color;
        _back.color = _color;
        Random.seed = (int)System.DateTime.Now.Ticks;
        PrepareDropItems(gameLevel);
        _levelPad.PickedIDList = _itemIDs;
        CreateAttributeValues(gameLevel);
    }

    // Create the boss's HP and ATK. 
    private void CreateAttributeValues(int gameLevel)
    {
        for (int i = 0; i < gameLevel - 1; i++)
        {
            _hitPoint *= 1.1f;
            _atk *= 1.1f;
            transform.localScale *= 1.01f;
        }
        _hitPointMax = _hitPoint;
    }

    // Random get the boss's drop items.
    private void PrepareDropItems(int gameLevel)
    {
        int levelFloor = (gameLevel - 1) / LEVEL_RANGE + 1;
        Debug.Log("levelFloor: " + levelFloor);
        int itemAmount = gameLevel % LEVEL_RANGE;
        if (itemAmount == 0)
            itemAmount = LEVEL_RANGE;
        for (int i = 0; i < itemAmount; i++)
        {
            int id = Random.Range(0, levelFloor + 1);
            Debug.Log("id = Random.Range: " + id);
            if (id != 0)
                _itemIDs.Add(id);
            if (id > _levelPad.MaxItemID)
                id = _levelPad.MaxItemID;
        }
        Debug.Log(DropItemsCount);
    }

    // Get the boss's color.
    public Color GetColor(bool isWithAlpha)
    {
        Color color = _color;
        if (!isWithAlpha)
            color.a = 1f;
        return color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit by hero(ball).
        if (collision.gameObject.name == "Ball")
            _table.Bang(collision.relativeVelocity.magnitude);
        transform.Translate(-collision.relativeVelocity / 100f);
    }

    public float HP
    {
        get { return _hitPoint; }
        set 
        { 
            _hitPoint = value;
            _color.a = 2f * (_hitPoint / _hitPointMax);
            _sprite.color = _color;
        }
    }

    public float ATK
    {
        get { return _atk; }
        set { _atk = value; }
    }

    public int DropItemsCount 
    {
        get { return _itemIDs.Count; }
    }
}
