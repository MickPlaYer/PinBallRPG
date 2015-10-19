using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {
    public int _menuIndex = 2;
    public int _buttonNum;
    public GameObject[] _panels = new GameObject[7];
    GameObject[] _thingsToTransform = new GameObject[10];
    int _temp = 0,_nextScene;
    float _time = 0;
    GameObject _nowScene;
    GameObject[] _tempTrans = new GameObject[10];
    bool flag = false;
    void Awake()
    {

        _nowScene = _panels[_menuIndex];
        foreach(GameObject g in _panels)
        {
            g.SetActive(false);
        }
        _panels[_menuIndex].SetActive(true);
        for (int i = 0; i < _nowScene.GetComponent<ThingToTransform>()._thingsToTransform.Length; i++)
        {
            _thingsToTransform[i] = _nowScene.GetComponent<ThingToTransform>()._thingsToTransform[i];
        }
    }
    
    void Update()
    {
        _time += Time.deltaTime;
        
        if (flag && _time < 0.5f)
        {
            
            while (_time > 0.1f && _temp< _nowScene.GetComponent<ThingToTransform>()._thingsToTransform.Length)
            {
                _tempTrans[_temp] = (GameObject)Instantiate( _thingsToTransform[_temp], _thingsToTransform[_temp].transform.position, _thingsToTransform[_temp].transform.rotation);
                _tempTrans[_temp].transform.SetParent( _nowScene.transform);
                _thingsToTransform[_temp].SetActive(false);
                _time = 0;
                _temp++;
            }
            PanelTransform();
        }
            
        else if(flag&&_time>0.5f )
        {

            _temp = 0;
            _nowScene.SetActive(false);
           
            foreach(GameObject i in _thingsToTransform)
            {
                if(i!=null)
                i.SetActive(true);
            }
            _nowScene = _panels[_nextScene];

            for (int i = 0; i < _tempTrans.Length; i++)
            {
                Destroy(_tempTrans[i]);
            }
            for (int i =0;i<_thingsToTransform.Length;i++)
            {
                if (i<_nowScene.GetComponent<ThingToTransform>()._thingsToTransform.Length)
                    _thingsToTransform[i] = _nowScene.GetComponent<ThingToTransform>()._thingsToTransform[i];
                else
                    _thingsToTransform[i] = null;
            }
            _panels[_nextScene].SetActive(true);
       
           
            flag = false;
        }
    }
	
    public void ChangeScene(int sceneNum)
    {
        _nextScene = sceneNum;
        _time = 0;
            flag = true;
    }

    void PanelTransform()
    {
        foreach(GameObject i in _tempTrans)
        {
            if(i != null)
            i.transform.Translate(-3600*Time.deltaTime, 0, 0);
        }    
    }

    public void SetPanelDefalt(GameObject panel)
    {
        panel.transform.position = new Vector2(0, 0);
        panel.SetActive(false);
    }

    public void loadBattleScene()
    {
        PlayerPrefs.Save();
        Application.LoadLevel(1);
    }
}
