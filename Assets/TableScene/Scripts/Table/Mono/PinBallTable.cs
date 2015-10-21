using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PinBallTable : MonoBehaviour
{
    private const int BASIC_SPEED = 20;
    private const int BALL_MOVE_UNIT = 1000;
    private HeroData _hero;
    private Vector2 _startPoint = new Vector2(7f, -5.5f);
    private Rigidbody2D _ballRD;
    private float _ballMoveCount = 0;
    private Hashtable _sounds = new Hashtable();
    private bool _isEnd = false;
    private int _flyNumberIndex = 0;
    public GameObject[] _hideObject;
    public ComboBox _comboBox;
    public float _deadBottom = -8.5f;
    public PinBallBar _barLeft;
    public PinBallBar _barRight;
    public Boss _boss;
    public Hole _hole;
    public GameObject _ball;
    public UILevelPad _levelPad;
    public FlyNumber[] _flyNumbers;
    public RecNumber _recumber;

    // Use this for initialization
    void Start()
    {
        _ballRD = _ball.GetComponent<Rigidbody2D>();
        _hero = new HeroData();
        PrepareSounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
            return;
        CheckBallDrop();
        ControlInput();
        if (_hero.HP < 0)
            return;
        DoBallMove();
    }

    // When hero is attack the boss.
    public void Bang(float speed)
    {
        int damage = GetHeroAttackDamage(speed);
        HurtHero((int)_boss.ATK);
        HurtBoss(damage);
    }

    // Hurt the hero.
    public void HurtHero(int damage)
    {
        _hero.HP -= damage;
        if (_hero.HP <= 0)
            OnHeroDead();
    }

    // Hurt the Boss.
    public void HurtBoss(int damage)
    {
        PlaySound("Bang");
        FlyANumber(_boss.transform.position, damage);
        _boss.HP -= damage;
        if (_boss.HP <= 0)
            OnBossDead();
    }

    // Get hero's attack damage by speed and combo.
    private int GetHeroAttackDamage(float speed)
    {
        float comboBonus = 1f + (_comboBox.Count * 0.1f);
        float speedBonus = speed / BASIC_SPEED;
        int damage = (int)(_hero.ATK * comboBonus * speedBonus);
        if (damage < 1)
            damage = 1;
        return damage;
    }

    // Throw a fly number.
    private void FlyANumber(Vector2 from, int damage)
    {
        _flyNumbers[_flyNumberIndex].StartFly(from, damage);
        _flyNumberIndex++;
        if (_flyNumberIndex >= _flyNumbers.Length)
            _flyNumberIndex = 0;
    }

    // When Boss HP -> 0
    private void OnBossDead()
    {
        PlaySound("BossDie");
        _boss.gameObject.SetActive(false);
        _hole.gameObject.SetActive(true);
        MakeItemDrop();
        _ballRD.AddForce((_ball.transform.position - _boss.transform.position) * 500, ForceMode2D.Force);
    }

    // Throw out all drop items.
    private void MakeItemDrop()
    {
        GameObject ojb = Resources.Load<GameObject>("Item");
        int count = _boss.DropItemsCount;
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(ojb);
            item.GetComponent<ItemBall>().Hole = _hole;
            item.GetComponent<SpriteRenderer>().color = _boss.GetColor(false);
        }
    }

    // When Hero HP -> 0
    private void OnHeroDead()
    {
        if (_isEnd)
            return;
        ShutDownTable();
        PlaySound("ShutDown");
        _isEnd = true;
    }

    // Shut down the objects on the table.
    private void ShutDownTable()
    {
        foreach (GameObject obj in _hideObject)
            obj.SetActive(false);
        _barLeft.ShutDown();
        _barRight.ShutDown();
        _comboBox.ShutDown();
    }

    // Set ball to start point.
    private void SetBallToStartPoint()
    {
        _ballRD.velocity = Vector2.zero;
        _ballRD.angularVelocity = 0f;
        _ball.transform.position = _startPoint;
    }

    // Ball's behavior.
    private void DoBallMove()
    {
        OnBallMoveUnit();
        CheckBallInHole();
    }

    // When ball moved a number of length.
    private void OnBallMoveUnit()
    {
        if (_hero.HP <= 0)
            return;
        _ballMoveCount += _ballRD.velocity.magnitude;
        if (_ballMoveCount >= BALL_MOVE_UNIT)
        {
            // Hero do recover.
            if (_hero.HP < _hero.MaxHP)
                _recumber.Show((int)_hero.REC);
            _hero.HP += _hero.REC;
            _ballMoveCount -= BALL_MOVE_UNIT;
        }
    }

    // Check ball droping down from table.
    private void CheckBallDrop()
    {
        if (_ball.transform.position.y < _deadBottom)
        {
            _hero.HP -= _boss.ATK * 5;
            if (_hero.HP <= 0)
            {
                OnHeroDead();
                _levelPad.ShowFailPad();
            }
            else
            {
                PlaySound("DropDown");
                SetBallToStartPoint();
            }
        }
    }

    // Check ball droppong in hole.
    private void CheckBallInHole()
    {
        if (!_ballRD.isKinematic)
            StuckAtHole();
        else
            BeforeNextLevel();
    }

    // Stuck ball into hole when it can.
    private void StuckAtHole()
    {
        Vector2 length = _hole.transform.position - _ball.transform.position;
        float speed = _ballRD.velocity.magnitude;
        if (length.magnitude < 0.1f && speed < 1)
        {
            _ball.transform.position = _hole.transform.position;
            _ballRD.isKinematic = true;
            PlaySound("InToHole");
        }
    }

    // Check item balls are all get into the hole.
    private void BeforeNextLevel()
    {
        _ball.transform.localScale *= 0.9f;
        if (_ball.transform.localScale.x < 0.01f)
        {
            if (GameObject.Find("Item(Clone)") == null)
            {
                if (_isEnd)
                    return;
                ShutDownTable();
                _levelPad.ShowSuccessPad();
                _isEnd = true;
            }
        }
    }

    // Contorl the input.
    private void ControlInput()
    {
        if (_isEnd)
            return;
        _barLeft.ControlInput();
        _barRight.ControlInput();
    }

    // Load sounds into hash table.
    private void PrepareSounds()
    {
        Object[] clips = Resources.LoadAll("Sounds/Table");
        foreach (var clip in clips)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip as AudioClip;
            _sounds.Add(clip.name, source);
        }
    }

    // Play sound from hash table.
    private void PlaySound(string hash)
    {
        (_sounds[hash] as AudioSource).Play();
    }
}
