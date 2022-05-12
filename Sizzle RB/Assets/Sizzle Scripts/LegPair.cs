using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPair : MonoBehaviour
{
    public Transform axisFront;
    public Transform axisBack;

    // Vector between back and front 
    private Vector3 axisVector { get { return axisFront.position - axisBack.position; } }
    private Vector3 root {  get { return axisBack.position + (axisVector * percentAlong); } }

    [Range(0, 1), Tooltip("The distance along the vector created by the front and back axis")]
    public float percentAlong;

    // What are actually going to be moved 
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;

    public Vector3 jointOffset;
    public Vector3 footOffset;

    public float downMax;

    [Tooltip("X Axis is min and Y Axis is max")]
    public Vector2 randomOffsetRange;


    private Vector3 jointPosLeft;
    private Vector3 jointPosRight;

    private Vector3 LeftRoot { get { return root + (axisVector.normalized * footOffset.z) + (axisBack.up * footOffset.y + axisBack.right * footOffset.x); } }
    private Vector3 RightRoot { get { return root + (axisVector.normalized * footOffset.z) + (axisBack.up * footOffset.y + -axisBack.right * footOffset.x); } }
    private Vector3 holdPosLeft;
    private Vector3 holdPosRight;





    [Tooltip("How far can the new pos be from hold point ")]
    public float maxDisFromHold;
    [Tooltip("Could change to be calculated by bone lengths ")]
    public float MaxDisFromFloor;
    public LayerMask floor;

    [Tooltip("How far can it be from hold point before moving ")]
    public float footTolerance; 
    public float footMoveForce;
    public float footLiftForce;

    // Start is called before the first frame update
    void Start()
    {
        // Sets start positions to each side along normal 

        RaycastHit hit;
        Physics.Raycast(LeftRoot, Vector3.down, out hit, MaxDisFromFloor, floor);

        holdPosLeft = hit.point;;
        //holdPosRight =
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitLeft;
        Physics.Raycast(LeftRoot, Vector3.down, out hitLeft, MaxDisFromFloor, floor);
        print(Vector3.Distance(hitLeft.point, holdPosLeft));
        if(Vector3.Distance(hitLeft.point, holdPosLeft) > maxDisFromHold)
        {
            holdPosLeft = hitLeft.point;
            leftFoot.AddForce(footLiftForce * Vector3.up, ForceMode.Impulse);
            print("Impulse");
        }

        if(Vector3.Distance(leftFoot.transform.position, holdPosLeft) > footTolerance)
        {
            leftFoot.AddForce((holdPosLeft - leftFoot.position).normalized * footMoveForce, ForceMode.Force);
        }
        else
        {
            leftFoot.position = holdPosLeft;
        }

        // UpdateFoot(leftFoot, holdPosLeft, this.transform.position + startOffset);
    }

    private void UpdateFoot(Rigidbody foot, Vector3 holdPos, Vector3 newPos)
    {
        // Check if hold point needs to be updated 
        if (Vector3.Distance(holdPos, newPos) > maxDisFromHold)
        {
            // Get normal from new foot spot 
            RaycastHit hit;
            Physics.Raycast(LeftRoot, Vector3.down, out hit, MaxDisFromFloor);

            // Initial upward force 
            foot.AddForce(Vector3.up * footLiftForce, ForceMode.Impulse);
            print("Impulse");

            holdPos = newPos;
        }

        // Moves foot if necessary 
        print(Vector3.Distance(foot.transform.position, holdPos));
        if(Vector3.Distance(foot.transform.position, holdPos) >= footTolerance)
        {
            foot.AddForce((holdPos - leftFoot.transform.position).normalized * footMoveForce * Time.deltaTime, ForceMode.Force);
            print("force");
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 difference = axisVector.normalized - Vector3.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(root, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(LeftRoot, 0.1f);

        RaycastHit hit;
        Physics.Raycast(LeftRoot, Vector3.down, out hit, MaxDisFromFloor, floor);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, 0.1f);
        Gizmos.DrawLine(LeftRoot, hit.point);



        Gizmos.color = Color.gray;
        jointPosLeft = root + (axisVector.normalized * jointOffset.z) + (axisBack.up * jointOffset.y + axisBack.right * jointOffset.x);
        Gizmos.DrawSphere(jointPosLeft, 0.1f);
    }
}
