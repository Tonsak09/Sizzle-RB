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

    public Vector3 offset;
    public Vector3 offsetedRoot { get { return root + (axisVector.normalized * offset.z) + (axisBack.up * offset.y + axisBack.right * offset.x); } }

    public Vector3 IKOffset;
    private Vector3 offsetedIK { get { return root + (axisVector.normalized * IKOffset.z) + (axisBack.up * IKOffset.y + axisBack.right * IKOffset.x); } }

    public Vector3 IKHintOffset;
    private Vector3 offsetedIKHint { get { return root + (axisVector.normalized * IKHintOffset.z) + (axisBack.up * IKHintOffset.y + axisBack.right * IKHintOffset.x); } }

    // Moving info
    public float stepDistance;
    public float speed;

    private Vector3 origin;
    private Vector3 target;
    private float lerp;

    public LegIKSolver opposite;
    public float Lerp { get { return lerp; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IKStart.position = offsetedIK;
        IKHint.position = offsetedIKHint;

        RaycastHit hit;

        if(lerp >= 1 )
        {
            if (Physics.Raycast(offsetedRoot + Vector3.one * Random.Range(-randRange, randRange), Vector3.down, out hit, MaxDisFromFloor, mask))
            {
                if (Vector3.Distance(hit.point, target) > stepDistance)
                {
                    lerp = 0;
                    origin = end.transform.position;
                    target = hit.point;
                }
                else
                {
                    end.position = origin;
                }
            }
        }
        

        if(lerp < 1)
        {
            Vector3 footPos = Vector3.Lerp(origin, target, lerp);
            footPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            end.position = footPos;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            origin = target;
        }

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
    }
}
