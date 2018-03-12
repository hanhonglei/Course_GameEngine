using UnityEngine;
using System.Collections;

public class TargetCtrl : MonoBehaviour {

    protected Animator animator;

    //the platform object in the scene
    public Transform jumpTarget = null;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator)
        {
            animator.ResetTrigger("jump");
            if (Input.GetButton("Fire1"))
            {
                animator.SetTrigger("jump");
                Debug.Log("Fired!");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                animator.MatchTarget(jumpTarget.position + new Vector3(0, 0.5f, 0), jumpTarget.rotation, AvatarTarget.LeftFoot,
                                                       new MatchTargetWeightMask(Vector3.one, 1f), 0.35f, 0.5f);
                Debug.Log("AnimOK!");
            }
        }
    }

}
