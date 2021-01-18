using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Movement
{
    public class HeadIK : MonoBehaviour
    {
        Animator anim;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
        }

        void OnAnimatorIK(int layerIndex)
        {
            Transform headTransform = anim.GetBoneTransform(HumanBodyBones.Head);
            anim.SetLookAtWeight(1.0f);
            anim.SetLookAtPosition(headTransform.position + transform.forward);
        }
    }
}

