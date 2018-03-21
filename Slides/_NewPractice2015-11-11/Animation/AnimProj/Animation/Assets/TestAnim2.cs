using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim2 : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
	}
}
