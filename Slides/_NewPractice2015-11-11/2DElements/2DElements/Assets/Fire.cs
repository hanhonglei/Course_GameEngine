using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Rigidbody bullet;

	// Use this for initialization
	void Start () {
		
	}
    void FireBullet(Vector3 target)
    {
        //Vector3 dir = target - transform.position;
        //Vector3 xyProject = Vector3.ProjectOnPlane(dir, Vector3.up);

        //Vector3 bulletStartPos = transform.position;
        //bulletStartPos.y = 1;
        Rigidbody g = (Rigidbody)GameObject.Instantiate(bullet, transform.position
            , Quaternion.FromToRotation(Vector3.forward, target));
        g.velocity = target.normalized * 100;        
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("down");
            //Vector3 desPos;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            FireBullet(ray.direction);
            //float enter = 0.0f;
            //Plane firePlane = new Plane(Vector3.up, transform.position);
            //if (firePlane.Raycast(ray, out enter))
            //{
            //    desPos = ray.GetPoint(enter);
            //    FireBullet(desPos);
            //}
        }
		
	}
}
