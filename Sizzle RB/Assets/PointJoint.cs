using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointJoint : MonoBehaviour
{

    public LayerMask mask;

    public float floorOffset;
    public float adjustForce;

    private Rigidbody rb;

    private Vector3 groundNormal;
    public Vector3 GroundNormal { get { return groundNormal; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, floorOffset, mask))
        {
            //rb.AddForce(adjustForce * Vector3.down * Time.deltaTime, ForceMode.Force);
            this.transform.position = hit.point + Vector3.up * floorOffset;

            groundNormal = hit.normal;
        }
        else
        {
            rb.AddForce(Vector3.down * adjustForce * Time.deltaTime);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down * floorOffset));
    }
}
