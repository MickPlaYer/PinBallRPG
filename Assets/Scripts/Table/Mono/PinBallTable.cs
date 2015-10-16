using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PinBallTable : MonoBehaviour
{
    private const int BASIC_SPEED = 20;
    private const int BALL_MOVE_UNIT = 1000;
    private ItemBox _itemBox;
    private HeroData _hero;
    private Vector2 _startPoint = new Vector2(7f, -5.5f);
    private Rigidbody2D _ballRD;
    private int _gameLevel = 1;
    private float _ballMoveCount = 0;
    private List<GameObject> _itemList = new List<GameObject>();
    // private List<AudioSource> _sounds = new List<AudioSource>();
    private Hashtable _sounds = new Hashtable();
    public bool _resetSaves = false;
    public float bottom = -8.5f;
    public PinBallBar _barLeft;
    public PinBallBar _barRight;
    public BarController[] _controller;
    public Boss _boss;
    public Hole _hole;
    public GameObject _ball;
    public Cover _cover;

    // Use this for initialization
    void Start()
    {
        if (_resetSaves)
        {
            PlayerPrefs.DeleteAll();
        }
        _gameLevel = PlayerPrefs.GetInt("game_level", 1);
        _ballRD = _ball.GetComponent<Rigidbody2D>();
        _hero = new HeroData();
        _itemBox = new ItemBox();

        Object[] clips = Resources.LoadAll("Sounds/Table");
        foreach (var clip in clips)
        {
            Debug.Log(clip.name);
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip as AudioClip;
            _sounds.Add(clip.name, source);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_hero.HP < 0)
            return;
        ControlInput();
        DoBallMove();

        // Bellow is for Debug
        GetComponent<GUIText>().text = "Boss HP:" + _boss.HP.ToString() + "\nHero HP:" + _hero.HP.ToString();
        // Debug.DrawLine(_ball.transform.position, (Vector2)_ball.transform.position + _ballRD.velocity);
        // Debug.Log(_ballRD.velocity.magnitude);
    }

    // When hero is attack the boss.
    public void Bang(float value)
    {
        PlaySound("Bang");
        _boss.HP -= _hero.ATK * value / BASIC_SPEED;
        _hero.HP -= _boss.ATK;
        if (_hero.HP <= 0)
            OnHeroDead();
        if (_boss.HP <= 0)
            OnBossDead();
    }

    // Add items to item box.
    public void PickItem(int id, GameObject obj)
    {
        _itemBox.PickUpItem(id);
        _itemList.Remove(obj);
        Destroy(obj);
    }

    // When Boss HP -> 0
    private void OnBossDead()
    {
        _boss.gameObject.SetActive(false);
        _hole.gameObject.SetActive(true);
        _ballRD.AddForce((_ball.transform.position - _boss.transform.position) * 500, ForceMode2D.Force);
        GameObject ojb = Resources.Load<GameObject>("Item");
        for (int i = 0; i < 50; i++)
            _itemList.Add(Instantiate(ojb));
    }

    // When Hero HP -> 0
    private void OnHeroDead()
    {
        var left = GameObject.Find("BufferTriangleLeft/Buffer");
        if (left != null)
            left.SetActive(false);
        var right = GameObject.Find("BufferTriangleRight/Buffer");
        if (right != null)
            right.SetActive(false);
        _controller[0]._buttonHeld = false;
        _controller[1]._buttonHeld = false;
        _barLeft.ShutDown();
        _barRight.ShutDown();
        PlaySound("ShutDown");
    }

    // Play sound from hash table.
    private void PlaySound(string hash)
    {
        (_sounds[hash] as AudioSource).Play();
    }

    // Set ball to start point.
    private void SetBallToStartPoint()
    {
        _ballRD.velocity = Vector2.zero;
        _ball.transform.position = _startPoint;
    }

    // Ball's behavior.
    private void DoBallMove()
    {
        OnBallMoveUnit();
        CheckBallDrop();
        CheckBallInHole();
    }

    // When ball moved a number of length.
    private void OnBallMoveUnit()
    {
        _ballMoveCount += _ballRD.velocity.magnitude;
        if (_ballMoveCount >= BALL_MOVE_UNIT)
        {
            // Hero do recover.
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
                ShowLevelFailPad();
            }
            else
                SetBallToStartPoint();
        }
    }

    // Check ball droppong in hole.
    private void CheckBallInHole()
    {
        Vector2 vector = _hole.transform.position - _ball.transform.position;
        float speed = _ballRD.velocity.magnitude;
        if (!_ballRD.isKinematic)
        {
            if (vector.magnitude < 0.1f && speed < 1)
            {
                _ball.transform.position = _hole.transform.position;
                _ballRD.isKinematic = true;
                PlaySound("InToHole");
            }
        }
        else
        {
            if (_ball.transform.localScale.x < 0.01f)
            {
                if (_itemList.Count == 0)
                {
                    ToNextLevel();
                }
            }
            else 
                _ball.transform.localScale *= 0.9f;
        }
    }

    // Go to next level.
    private void ToNextLevel()
    {
        _itemBox.SaveItems();
        _gameLevel++;
        PlayerPrefs.SetInt("game_level", _gameLevel);
        PlayerPrefs.Save();
        Application.LoadLevel("PinBallTable");
    }

    // Contorl the input.
    private void ControlInput()
    {
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
    }

    private void ShowLevelFailPad()
    {

    }
}
