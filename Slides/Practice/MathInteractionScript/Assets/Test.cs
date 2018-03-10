using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().useGravity = false;		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonUp(1))
        {
            Debug.Log("Clicked!");
            GetComponent<Rigidbody>().useGravity = true;
        }		
	}
    public void Hit(Vector3 mousePos)
    {
        transform.Translate(Vector3.up * 5);
    }
}
