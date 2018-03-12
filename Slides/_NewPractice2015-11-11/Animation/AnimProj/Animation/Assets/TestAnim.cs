using UnityEngine;
using System.Collections;

public class TestAnim : MonoBehaviour {

    private Animator anm;

	// Use this for initialization
	void Start () {
        anm = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            anm.SetTrigger("Jump");
        }
	
	}
}
