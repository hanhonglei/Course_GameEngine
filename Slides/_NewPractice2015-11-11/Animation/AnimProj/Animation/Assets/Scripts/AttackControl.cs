using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour {
    Animator anim = null;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        Debug.Assert(anim);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            anim.SetLayerWeight(1, 1.0f);
        }
        else
        {
            anim.SetLayerWeight(1, 0.0f);
        }
	}
}
