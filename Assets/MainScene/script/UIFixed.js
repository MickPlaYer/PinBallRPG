#pragma strict
import UnityEngine.UI;

private var baseW : float = 640;
private var baseH : float = 960;
//比例
private var scale : Vector2;
function Start () {

}

function Update () {

}


function Awake(){
    scale = Vector2(parseFloat(Screen.width)/baseW,parseFloat(Screen.height)/baseH);
}

function OnGUI(){
  
    GUIUtility.ScaleAroundPivot(scale,Vector2.zero);
  
    /* .... 其他GUI ....*/
}