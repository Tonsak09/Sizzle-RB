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

    [SerializeField] float speed;
    [SerializeField] float maxTurnAngle;

    [SerializeField] float minTargetDisToMove;

    private void Start()
    {

    }

    void LateUpdate()
    {
        UpdateHead();
    }


    /// <summary>
    /// Adjusts the head to look towards the target position 
    /// </summary>
    private void UpdateHead()
    {
        // Store the current head rotation since we will be resetting it
        Quaternion currentLocalRotation = headBone.localRotation;
        // Reset the head rotation so our world to local space transformation will use the head's zero rotation. 
        // Note: Quaternion.Identity is the quaternion equivalent of "zero"
        headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = target.position - headBone.position;
        Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

        // Apply angle limit
        targetLocalLookDir = Vector3.RotateTowards(
          Vector3.forward,
          targetLocalLookDir,
          Mathf.Deg2Rad * maxTurnAngle, // Note we multiply by Mathf.Deg2Rad here to convert degrees to radians
          0 // We don't care about the length here, so we leave it at zero
        );

        // Get the local rotation by using LookRotation on a local directional vector
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        // Apply smoothing
        headBone.localRotation = Quaternion.Slerp(
          currentLocalRotation,
          targetLocalRotation,
          1 - Mathf.Exp(-speed * Time.deltaTime)
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target.position, 0.1f);
    }
}
