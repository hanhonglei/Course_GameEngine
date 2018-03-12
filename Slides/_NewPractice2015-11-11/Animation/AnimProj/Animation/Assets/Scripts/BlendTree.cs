using UnityEngine;
using System.Collections;

public class BlendTree : MonoBehaviour {

    protected Animator animator;
    private float speed = 0;
    private float dir = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += 2*Time.deltaTime;
            speed = Mathf.Clamp(speed, 0 , 1);
            Debug.Log("pressed");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= 2*Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir += 2 * Time.deltaTime;
            dir = Mathf.Clamp(dir, -1, 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir -= 2 * Time.deltaTime;
            dir = Mathf.Clamp(dir, -1, 1);
        }
        animator.SetFloat("speed", speed);
        animator.SetFloat("dir", dir);
	
	}
}
