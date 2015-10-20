using UnityEngine;
using System.Collections;


public class ItemCast : MonoBehaviour {
    public Dash _dash;
    public Shoot _shoot;
    public HeroData3 _heroData;
    public CDController [] _a ;
    public GameObject[] _mask;
    // Use this for initialization
    void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAbilityCD(int id,int index,GameObject item)
    {
        switch (id)
        {
            case 1:
                _a[index].SetCD(2f);
                break;

            case 2:
                _a[index].SetCD(1f);
                break;

            case 8:
                _a[index].SetCD(3);
                break;
            default:
                item.SetActive(false);
                break;


        }
               

    }

    public void Cast(int id,int index)
    {
       //Debug.Log(_a[index]._coldTime) ;
        if (!_a[index]._trigger)
        {
            switch (id)
            {
                case 1:
                    _dash.Cast(Vector2.left);
                    break;

                case 2:
                    _dash.Cast(Vector2.right);
                    break;
                case 8:
                    _shoot.Cast(40);
                    break;
            }

            _a[index].SetTrigger(true);
        }
        
    }

    public void a1()
    {
        Debug.Log("a1");        
    }

    public void a2()
    {
        Debug.Log("a2");
    }

    public void a3()
    {
        Debug.Log("a3");
    }


}
