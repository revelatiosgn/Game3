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

        Vector3 position;

        void Awake()
        {
            anim = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();

            position = transform.position;
        }

        void Update()
        {
            Vector3 delta = transform.position - position;
            Debug.Log(delta / Time.deltaTime);
            position = transform.position;

            // Vector3 velocity = characterController.velocity.normalized;

            // ascending = Mathf.SmoothDamp(ascending, 0.5f + velocity.y, ref smoothSpeed, smoothTime);

            // float ascending =  0.5f + velocity.y;

            // Vector3 v = Vector3.ProjectOnPlane(characterController.velocity, Vector3.up);
            // float m = v.magnitude / 5.0f;

            // anim.SetFloat("vertical", m);
        }
    }
}

