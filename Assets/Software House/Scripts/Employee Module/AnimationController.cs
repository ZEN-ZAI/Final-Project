using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ResetState()
    {
        animator.SetBool("idle", false);
        animator.SetBool("walk", false);
        animator.SetBool("sit", false);
        animator.SetBool("work", false);
        animator.SetBool("sleep", false);
    }

    public void Idle()
    {
        ResetState();
        animator.SetBool("idle",true);
    }

    public void Walk()
    {
        ResetState();
        animator.SetBool("walk", true);
    }

    public void Sit()
    {
        ResetState();
        animator.SetBool("sit", true);
    }

    public void Work()
    {
        ResetState();
        animator.SetBool("work", true);
    }

    public void Sleep()
    {
        ResetState();
        animator.SetBool("sleep", true);
    }
}
