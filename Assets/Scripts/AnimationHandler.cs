using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSRPG
{
    public class AnimationHandler : MonoBehaviour
    {
        [Range(0f, 1f)] public float smoothTime = 0.5f;

        Animator anim;
        CharacterController characterController;

        float ascending = 0.5f;
        float smoothSpeed;

        void Awake()
        {
            anim = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Vector3 velocity = characterController.velocity.normalized;

            ascending = Mathf.SmoothDamp(ascending, 0.5f + velocity.y, ref smoothSpeed, smoothTime);

            // float ascending =  0.5f + velocity.y;

            anim.SetFloat("ascending", ascending);
        }
    }
}

