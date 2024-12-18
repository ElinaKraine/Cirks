using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RollDice()
    {
        animator.SetBool("IsRolling", true);
    }

    public void StopRoll()
    {
        animator.SetBool("IsRolling", false);
    }
}
