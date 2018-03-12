using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManage : MonoBehaviour {

    public Text text;
    public Slider mainSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void EnterGame()
    {
        Application.LoadLevel(1);
    }
    public void AdjustValue()
    {
        //Debug.Log("OK");
        //int vv = 20+(int)(v*10);
        text.fontSize = (int)mainSlider.value;

        Debug.Log(mainSlider.value);
    }
}
