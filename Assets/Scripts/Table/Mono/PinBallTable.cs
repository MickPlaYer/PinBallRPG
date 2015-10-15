using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PinBallTable : MonoBehaviour
{
    private const int BASIC_SPEED = 20;
    private const int BALL_MOVE_UNIT = 1;
    private ItemBox _itemBox;
    private HeroData _hero;
    private Vector2 _startPoint = new Vector2(7f, -5.5f);
    private Rigidbody2D _ballRD;
    private int _gameLevel = 1;
    private float _ballMoveCount = 0;
    private List<GameObject> _itemList = new List<GameObject>();
    public bool _resetSaves = false;
    public float bottom = -8.5f;
    public PinBallBar _barLeft;
    public PinBallBar _barRight;
    public BarController[] _controller;
    public Boss _boss;
    public Hole _hole;
    public GameObject _ball;

    // Use this for initialization
    void Start()
    {
        // _itemBox.PrintItemDebug();
        if (_resetSaves)
        {
            PlayerPrefs.DeleteAll();
        }
        _gameLevel = PlayerPrefs.GetInt("game_level", 1);
        _ballRD = _ball.GetComponent<Rigidbody2D>();
        _hero = new HeroData();
        _itemBox = new ItemBox();
    }

    // Update is called once per frame
    void Update()
    {
        ControlInput();
        OnBallMoved();

        GetComponent<GUIText>().text = "Boss HP:" + _boss.HP.ToString() + "\nHero HP:" + _hero.HP.ToString();
        Debug.DrawLine(_ball.transform.position, (Vector2)_ball.transform.position + _ballRD.velocity);
        // Debug.Log(_ballRD.velocity.magnitude);
    }

    // When hero is attack the boss.
    public void Bang(float value)
    {
        _boss.HP -= _hero.ATK * value / BASIC_SPEED;
        _hero.HP -= _boss.ATK;
        if (_boss.HP <= 0)
            OnBossDead();
    }

    private void OnBossDead()
    {
        _boss.gameObject.SetActive(false);
        _hole.gameObject.SetActive(true);
        // GameObject.Find("ExplosionMobile").SetActive(true);
        _ballRD.AddForce((_ball.transform.position - _boss.transform.position) * 500, ForceMode2D.Force);
        GameObject ojb = Resources.Load<GameObject>("Item");
        for (int i = 0; i < 50; i++)
            _itemList.Add(Instantiate(ojb));
    }

    private void OnHeroDead()
    {
        _ball.gameObject.SetActive(false);
    }

    private void SetBallToStartPoint()
    {
        _ballRD.velocity = Vector2.zero;
        _ball.transform.position = _startPoint;
    }

    private void OnBallMoved()
    {
        if (_ballRD == null)
            Debug.Log("asdasd");
        _ballMoveCount += _ballRD.velocity.magnitude;
        if (_ballMoveCount >= BALL_MOVE_UNIT)
        {
            _ballMoveCount -= BALL_MOVE_UNIT;
        }
        CheckBallDrop();
        CheckBallInHole();
    }

    private void CheckBallDrop()
    {
        if (_ball.transform.position.y < bottom)
        {
            _hero.HP -= _boss.ATK * 5;
            if (_hero.HP <= 0)
                OnHeroDead();
            else
                SetBallToStartPoint();
        }
    }

    private void CheckBallInHole()
    {
        Vector2 vector = _hole.transform.position - _ball.transform.position;
        float speed = _ballRD.velocity.magnitude;
        if (vector.magnitude < 0.1f && speed < 1)
        {
            _ball.transform.position = _hole.transform.position;
            _ballRD.isKinematic = true;
        }
        if (_ballRD.isKinematic)
        {
            _ball.transform.localScale *= 0.9f;
        }
        if (_ball.transform.localScale.x < 0.01f)
        {
            if (_itemList.Count == 0)
            {
                _itemBox.SaveItems();
                _gameLevel++;
                PlayerPrefs.SetInt("game_level", _gameLevel);
                PlayerPrefs.Save();
                Application.LoadLevel(0);
            }
        }
    }

    private void ControlInput()
    {
        if (_controller[0].GetButton())
            _barLeft.GoUp(_controller[0]);
        if (_controller[1].GetButton())
            _barRight.GoUp(_controller[1]);
        if (Input.GetKey(KeyCode.A))
            _barLeft.GoUp(KeyCode.A);
        if (Input.GetKey(KeyCode.D))
            _barRight.GoUp(KeyCode.D);
    }

    public void PickItem(int id, GameObject obj)
    {
        _itemBox.PickUpItem(id);
        _itemList.Remove(obj);
        Destroy(obj);
    }
}
