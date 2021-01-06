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
        movement.State = PlayerMovement.MovementState.Aim;
        animator.Play("ShieldDefenceBegin");
    }

    public void DefenceEnd()
    {
        movement.State = PlayerMovement.MovementState.Regular;
        animator.Play("None", 2);
    }
}
