using UnityEngine;
using System.Collections;


public class ItemCast : MonoBehaviour {
    public HeroData3 _heroData;
    public CDController [] _a ;
 
    // Use this for initialization
    void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAbilityCD(int id,int index)
    {
        switch (id)
        {
            case 1:
                _a[index].SetCD(2f);
                break;

            case 2:
                _a[index].SetCD(1f);
                break;

            case 3:
                _a[index].SetCD(1);
                break;

        }
               

    }

    public void Cast(int id,int index)
    {
       Debug.Log(_a[index]._coldTime) ;
        if (!_a[index]._trigger)
        {
            switch (id)
            {
                case 1:
                    a1();
                    break;

                case 2:
                    a2();
                    break;
                case 3:
                    a3();
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
