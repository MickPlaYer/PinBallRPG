using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    private float _hitPoint = 50;
    private float _atk = 10;
    public PinBallTable _table;
    public SpriteRenderer _sprite;

    // Use this for initialization
    void Start ()
    {
       int gameLevel = PlayerPrefs.GetInt("current_level", 0);
       for (int i = 0; i < gameLevel - 1; i++) 
       {
           _hitPoint *= 1.1f;
           _atk *= 1.1f;
           transform.localScale *= 1.01f;
       }
       Random.seed = gameLevel;
       _sprite.color = new Color(Random.value, Random.value, Random.value);
       Random.seed = (int)(Time.time * 1000);
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
