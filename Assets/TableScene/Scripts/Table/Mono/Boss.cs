using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    private const int LEVEL_RANGE = 4;
    private const float GROW_VALUE = 1.3f;
    private const float GROW_SIZE = 1.01f;
    private float _hitPoint = 50;
    private float _hitPointMax = 50;
    private float _atk = 10;
    private List<int> _itemIDs = new List<int>();
    private Color _color;
    public PinBallTable _table;
    public UILevelPad _levelPad;
    public SpriteRenderer _sprite;
    public SpriteRenderer _ring;
    public SpriteRenderer _shadow;

    // Use this for initialization
    void Start()
    {
        int gameLevel = PlayerPrefs.GetInt("current_level", 0);
        RandomColor(gameLevel);
        PrepareDropItems(gameLevel);
        _levelPad.PickedIDList = _itemIDs;
        CreateAttributeValues(gameLevel);
    }

    // Set color.
    private void RandomColor(int gameLevel)
    {
        Random.seed = gameLevel;
        _color = new Color(Random.value, Random.value, Random.value);
        _sprite.color = _color;
        _ring.color = _color;
        _shadow.color = _color;
        Random.seed = (int)System.DateTime.Now.Ticks;
    }

    // Create the boss's HP and ATK. 
    private void CreateAttributeValues(int gameLevel)
    {
        for (int i = 0; i < gameLevel - 1; i++)
        {
            _hitPoint *= GROW_VALUE;
            _atk *= GROW_VALUE;
            transform.localScale *= GROW_SIZE;
        }
        _hitPointMax = _hitPoint;
    }

    // Random get the boss's drop items.
    private void PrepareDropItems(int gameLevel)
    {
        int levelFloor = (gameLevel - 1) / LEVEL_RANGE + 1;
        int itemAmount = gameLevel % LEVEL_RANGE;
        if (itemAmount == 0)
            itemAmount = LEVEL_RANGE;
        for (int i = 0; i < itemAmount; i++)
        {
            int id = Random.Range(0, levelFloor + 1);
            if (id != 0)
                _itemIDs.Add(id);
            if (id > _levelPad.MaxItemID)
                id = _levelPad.MaxItemID;
        }
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

    // Get hp and set hp then reflash color alpha.
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
