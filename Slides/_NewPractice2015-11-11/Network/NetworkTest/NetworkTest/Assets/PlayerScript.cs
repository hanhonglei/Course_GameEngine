using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour {
    private Rigidbody rb;
    public float speed = 2;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * speed
            , Input.GetAxis("Vertical") * speed, 0) * Time.deltaTime;
        rb.MovePosition(transform.position + move);
	
	}
}
