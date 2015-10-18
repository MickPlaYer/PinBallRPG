using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CanvasFixed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Awake()
    {

        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

        float screenWidthScale = Screen.width / canvasScaler.referenceResolution.x;
        float screenHeightScale = Screen.height / canvasScaler.referenceResolution.y;
        canvasScaler.matchWidthOrHeight = 0;
       // canvasScaler.matchWidthOrHeight = screenWidthScale > screenHeightScale ? 0 : 1;
    }
}
