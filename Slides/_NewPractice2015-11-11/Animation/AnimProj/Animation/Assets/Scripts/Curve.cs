using UnityEngine;
using System.Collections;
public class Curve : MonoBehaviour {

    private Transform Head;
	// Use this for initialization
	void Start () {
        Head = transform.Find("HeadSphere");
	}
	
	// Update is called once per frame
	void Update () {
		float curve = GetComponent<Animator> ().GetFloat("TestCurve");
        Head.localScale = new Vector3(1+curve, 1+curve, 1+curve);
		
	}
}
