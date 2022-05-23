using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.weaverdev.io/blog/bonehead-procedural-animation

public class SkeletonController : MonoBehaviour
{
    // The target we are going to track
    [SerializeField] Transform target;
    // A reference to the gecko's neck
    [SerializeField] Transform headBone;

    void LateUpdate()
    {
        Vector3 towardObjectFromHead = target.position - headBone.position;

        headBone.rotation = Quaternion.LookRotation(towardObjectFromHead, transform.up);

    }
}