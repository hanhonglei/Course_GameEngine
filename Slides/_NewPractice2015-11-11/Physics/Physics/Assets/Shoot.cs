using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject bullet;
    public int speed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(bullet, transform.position, transform.rotation);
            b.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
		
	}
}
