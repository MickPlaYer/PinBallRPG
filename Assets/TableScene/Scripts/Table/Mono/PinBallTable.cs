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
    public ComboBox _comboBox;
    public int _debugLevel = 0;
    public float bottom = -8.5f;
    public PinBallBar _barLeft;
    public PinBallBar _barRight;
    public BarController[] _controller;
    public Boss _boss;
    public Hole _hole;
    public GameObject _ball;
    public UILevelPad _levelPad;
    public FlyNumber[] _flyNumbers;
    public RecNumber _recumber;

    // Use this for initialization
    void Start()
    {
        if (_debugLevel != 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("current_level", _debugLevel);
        }
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
        // Bellow is for Debug
        GetComponent<GUIText>().text = "Boss HP:" + _boss.HP.ToString() + "\nHero HP:" + _hero.HP.ToString();
        // Debug.DrawLine(_ball.transform.position, (Vector2)_ball.transform.position + _ballRD.velocity);
    }

    // When hero is attack the boss.
    public void Bang(float speed)
    {
        PlaySound("Bang");
        int damage = GetHeroAttackDamage(speed);
        _boss.HP -= damage;
        _hero.HP -= _boss.ATK;
        if (_hero.HP <= 0)
            OnHeroDead();
        if (_boss.HP <= 0)
            OnBossDead();
        FlyANumber(_boss.transform.position, damage);
    }

    // Get hero's attack damage by speed and combo.
    private int GetHeroAttackDamage(float speed)
    {
        float comboBonus = 1f + (_comboBox.Count * 0.25f);
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
        foreach (int id in _boss.GetDropItems())
        {
            GameObject item = Instantiate(ojb);
            var itemBall = item.GetComponent<ItemBall>();
            itemBall.SetRelatedObject(_levelPad, _hole);
            itemBall.ID = id;
            _levelPad.AddItem(item);
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
        var OptionButton = GameObject.Find("OptionButton");
        OptionButton.SetActive(false);
        var leftBuffer = GameObject.Find("BufferTriangleLeft/Buffer");
        if (leftBuffer != null)
            leftBuffer.SetActive(false);
        var rightBuffer = GameObject.Find("BufferTriangleRight/Buffer");
        if (rightBuffer != null)
            rightBuffer.SetActive(false);
        _controller[0]._buttonHeld = false;
        _controller[1]._buttonHeld = false;
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
        if (_ball.transform.position.y < bottom)
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
            if (_levelPad.IsAllItemPicked)
            {
                if (_isEnd)
                    return;
                ShutDownTable();
                _levelPad.ShowSuccessPad();
                _isEnd = true;
            }
    }

    // Contorl the input.
    private void ControlInput()
    {
        if (_isEnd)
            return;
        // For mobiles.
        if (_controller[0].GetButton())
            _barLeft.GoUp(_controller[0]);
        if (_controller[1].GetButton())
            _barRight.GoUp(_controller[1]);
        // For PC debug.
        if (Input.GetKey(KeyCode.A))
            _barLeft.GoUp(KeyCode.A);
        if (Input.GetKey(KeyCode.D))
            _barRight.GoUp(KeyCode.D);
        if (Input.GetKey(KeyCode.Escape))
            _levelPad.LoadMenuScene();
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
