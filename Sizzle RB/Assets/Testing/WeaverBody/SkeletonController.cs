using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.weaverdev.io/blog/bonehead-procedural-animation

public class SkeletonController : MonoBehaviour
{
    // The target we are going to track
    [SerializeField] Transform target;
    private Vector3 heldTargetPos;
    private Vector3 currentAim;
    public float lerp;
    private Vector3 origin;

    // A reference to the gecko's neck
    [SerializeField] Transform headBone;

    [SerializeField] float speed;
    [SerializeField] float maxTurnAngle;
    [SerializeField] float offsetRightAngle;
    [SerializeField] float offsetUpAngle;

    [SerializeField] float minTargetDisToMove;

    private void Start()
    {
        origin = target.position;

        heldTargetPos = target.position;
        currentAim = target.position;

        lerp = 0;
    }

    void LateUpdate()
    {
        LerpTarget();
        LookAtTarget();
    }

    private void LerpTarget()
    {
        // If target has moved in world space
        if (Vector3.Distance(heldTargetPos, target.position) >= minTargetDisToMove)
        {
            UpdateAim();
        }
        else
        {
            if(lerp >= 0.9)
            {
                UpdateAim();
            }
        }

        if(lerp <= 1)
        {
            currentAim = Vector3.Lerp(origin, heldTargetPos, lerp);

            lerp += Time.deltaTime * speed;
        }
        else
        {
            origin = heldTargetPos;
        }
        
    }

    private void UpdateAim()
    {
        float disFromOrigin = Vector3.Distance(heldTargetPos, origin);
        float totalDis = Vector3.Distance(target.position, origin);

        // Updates lerp to account for changed target 
        lerp = disFromOrigin / totalDis;
        heldTargetPos = target.position;
    }


    /// <summary>
    /// Adjusts the head to look towards the target position 
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 towardObjectFromHead = currentAim - headBone.position;

        headBone.rotation = Quaternion.LookRotation(towardObjectFromHead, -transform.up);

        // Adjusts for y-axis being forward and not z-axis
        headBone.eulerAngles -= Vector3.right * 90;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentAim, 0.1f);
    }
}
