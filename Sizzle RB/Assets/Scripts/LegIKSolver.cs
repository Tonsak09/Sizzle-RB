using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIKSolver : MonoBehaviour
{
    public LayerMask mask;
    
    public Transform IKStart;
    public Transform IKHint;
    public Transform end;
    public float stepHeight;

    public float randRange;

    // Setting up axis settings 
    public Transform axisFront;
    public Transform axisBack;

    public float MaxDisFromFloor;

    [Range(0, 1), Tooltip("The distance along the vector created by the front and back axis")]
    public float percentAlong;

    private Vector3 axisVector { get { return axisFront.position - axisBack.position; } }
    private Vector3 root { get { return axisBack.position + (axisVector * percentAlong); } }

    public Vector3 rootOffset;
    public Vector3 offsetedRoot { get { return root + (axisVector.normalized * rootOffset.z) + (axisBack.up * rootOffset.y + axisBack.right * rootOffset.x); } }

    public Vector3 IKOffset;
    private Vector3 offsetedIK { get { return root + (axisVector.normalized * IKOffset.z) + (axisBack.up * IKOffset.y + axisBack.right * IKOffset.x); } }

    public Vector3 IKHintOffset;
    private Vector3 offsetedIKHint { get { return root + (axisVector.normalized * IKHintOffset.z) + (axisBack.up * IKHintOffset.y + axisBack.right * IKHintOffset.x); } }

    public Vector3 startoffset;
    private Vector3 offsetedStart { get { return root + (axisVector.normalized * startoffset.z) + (axisBack.up * startoffset.y + axisBack.right * startoffset.x); } }

    // Moving info
    public float stepDistance;
    public float speed;

    private Vector3 origin;
    private Vector3 target;
    private float lerp;

    public LegIKSolver opposite;
    public float Lerp { get { return lerp; } }
    public bool Moving;

    // Start is called before the first frame update
    void Start()
    {
        target = offsetedStart;
        lerp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IKStart.position = offsetedIK;
        IKHint.position = offsetedIKHint;

    }

    public void TryMove()
    {
        if(Moving)
        {
            return;
        }

        RaycastHit hit;

        // Checking for new position 
        if (Physics.Raycast(offsetedRoot + Vector3.one * Random.Range(-randRange, randRange), Vector3.down, out hit, MaxDisFromFloor, mask))
        {
            // New position found 
            if (Vector3.Distance(hit.point, target) > stepDistance)
            {
                lerp = 0;
                //origin = end.transform.position;
                target = hit.point;

                StartCoroutine(Move());
            }
            else
            {
                end.position = origin;
            }
        }
    }

    private IEnumerator Move()
    {
        Moving = true;

        while(lerp <= 1)
        {
            // Moves smoothly to new point 
            Vector3 footPos = Vector3.Lerp(origin, target, lerp);
            footPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            end.position = footPos;
            lerp += Time.deltaTime * speed;
            yield return null;
        }

        // Once point is reached 
        end.transform.position = target;
        origin = target;
        Moving = false;
    }

    private void OnDrawGizmos()
    {
        //Vector3 difference = axisVector.normalized - Vector3.forward;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(root, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(offsetedRoot, 0.1f);
        Gizmos.DrawSphere(offsetedIK, 0.1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(offsetedIKHint, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(offsetedStart, 0.1f);
    }
}
