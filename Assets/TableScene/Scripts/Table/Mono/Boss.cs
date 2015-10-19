using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    const int LEVEL_RANGE = 3;
    private float _hitPoint = 50;
    private float _atk = 10;
    private List<int> _itemIDs = new List<int>();
    public PinBallTable _table;
    public SpriteRenderer _sprite;

    // Use this for initialization
    void Start()
    {
        int gameLevel = PlayerPrefs.GetInt("current_level", 0);
        Random.seed = gameLevel;
        _sprite.color = new Color(Random.value, Random.value, Random.value);
        Random.seed = (int)(Time.time * 1000);
        PrepareDropItems(gameLevel);
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
    }

    // Random get the boss's drop items.
    private void PrepareDropItems(int gameLevel)
    {
        int levelFloor = (gameLevel - 1) / LEVEL_RANGE;
        int itemAmount = gameLevel % LEVEL_RANGE;
        if (itemAmount == 0)
            itemAmount = LEVEL_RANGE;
        for (int i = 0; i < itemAmount; i++)
        {
            int id = Random.Range(0, LEVEL_RANGE + 1);
            if (id != 0)
            {
                id += levelFloor * 3;
                _itemIDs.Add(id);
            }
        }
    }

    // Return the drop item IDs.
    public int[] GetDropItems()
    {
        return _itemIDs.ToArray();
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
        set { _hitPoint = value; }
    }

    public float ATK
    {
        get { return _atk; }
        set { _atk = value; }
    }
}
