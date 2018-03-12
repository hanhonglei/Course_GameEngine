using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
    public Camera mainC;
    public Camera topC;
    public Camera smallC;
    public Camera orthC;
	// Use this for initialization
	void Start () {
        mainC.enabled = true;
        topC.enabled = false;
        smallC.enabled = false;
        orthC.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            EnableSmallC();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (orthC.enabled)
            {
                orthC.orthographic = !orthC.orthographic;
            }
        }
	}
    void SwitchCameras(){
        if (mainC.enabled)
        {
            mainC.enabled = false;
            topC.enabled = true;
            return;
        }
        else if (topC.enabled)
        {
            topC.enabled = false;
            orthC.enabled = true;
            return;
        }
        else
        {
            orthC.enabled = false;
            mainC.enabled = true;
        }
    }
    void EnableSmallC()
    {
        smallC.enabled = !smallC.enabled;
    }
}
