using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Movement;

public class ShieldBehaviour : MonoBehaviour
{
    Animator animator;
    PlayerMovement movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void DefenceBegin()
    {
        animator.SetBool("defence", true);
        GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Aim;
    }

    public void DefenceEnd()
    {
        animator.SetBool("defence", false);
        GetComponent<PlayerMovement>().state = PlayerMovement.MovementState.Regular;
    }
}
