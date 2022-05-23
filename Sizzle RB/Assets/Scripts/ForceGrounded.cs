using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGrounded : MonoBehaviour
{

    public LayerMask mask;

    public float maxDisFromGround;
    public float downwardForce;

    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Physics.Raycast(this.transform.position, Vector3.down, maxDisFromGround, mask))
        {
            rb.AddForce(downwardForce * Vector3.down * Time.deltaTime, ForceMode.Acceleration);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.down * maxDisFromGround);
    }
}
