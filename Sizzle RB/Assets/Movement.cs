using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    private PointJoint pj; // Every movement should have a PJ but not every PJ needs a movement 

    public float speed;
    public float rotSpeed;

    // Represents rotation on xz plane 
    private Vector3 direction;

    public Vector3 Direction { get { return direction; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        pj = this.GetComponent<PointJoint>();

        direction = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit, pj.floorOffset, pj.mask);
        //direction = Vector3.ProjectOnPlane(direction, hit.normal);

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(speed * direction * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-speed * direction * Time.deltaTime, ForceMode.Force);
        }
        

        if(Input.GetKey(KeyCode.D))
        {
            //rb.AddTorque(rotSpeed * Vector3.up * Time.deltaTime, ForceMode.Force);
            direction = Maths.RotateVectorXZ(direction, -rotSpeed * Time.deltaTime);
            rb.velocity = Vector3.Project(rb.velocity, direction);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //rb.AddTorque(-rotSpeed * Vector3.up * Time.deltaTime, ForceMode.Force);
            direction = Maths.RotateVectorXZ(direction, rotSpeed * Time.deltaTime);
            rb.velocity = Vector3.Project(rb.velocity, direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + direction * 5);
    }
}
