using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public float speed = 0.1F;
    bool bClick = false;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            bClick = true;
        }
        if (Input.GetMouseButtonUp(0))
            bClick = false;
        if(bClick)
        {
            Plane p = new Plane(transform.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            Vector3 desPos = new Vector3();
            if (p.Raycast(ray, out rayDistance))
            {
                Debug.Log(rayDistance);
                desPos = ray.GetPoint(rayDistance);
                Vector3 relativePos = desPos - transform.position;
                Quaternion to = Quaternion.LookRotation(relativePos);
                transform.rotation=Quaternion.Slerp(transform.rotation, to, speed*Time.deltaTime);
            }

        }
	}
}
