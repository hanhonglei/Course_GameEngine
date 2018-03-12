using UnityEngine;
using System.Collections;

public class RootM : MonoBehaviour {

    protected Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (animator)
        {
            if (Input.GetKeyDown("space"))
            {
                animator.SetTrigger("walk");
                animator.SetFloat("speed", 2.0f);
            }
        }
	}
    void OnAnimatorMove()
    {
        if (!animator)
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            return;
        }
        if (animator)
        {
            transform.Translate(Vector3.forward * animator.GetFloat("speed") *Time.deltaTime);
        }
    }
}
