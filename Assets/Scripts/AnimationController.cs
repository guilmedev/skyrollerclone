using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{



    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWinAnim()
    {
        animator.SetBool("Idle", false);

        animator.SetTrigger("Win");
    }

    public void SetDeathAnim()
    {
        animator.SetBool("Idle", false);

        animator.SetTrigger("Death");
    }

    public void SetIdleAnim()
    {
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
