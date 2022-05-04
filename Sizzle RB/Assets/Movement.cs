using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;
    public float rotSpeed;

    // Represents rotation on xz plane 
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        direction = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(speed * direction * Time.deltaTime, ForceMode.Force);
        }
        

        if(Input.GetKey(KeyCode.D))
        {
            //rb.AddTorque(rotSpeed * Vector3.up * Time.deltaTime, ForceMode.Force);
            direction = Maths.RotateVectorXZ(direction, -rotSpeed * Time.deltaTime);
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //rb.AddTorque(-rotSpeed * Vector3.up * Time.deltaTime, ForceMode.Force);
            direction = Maths.RotateVectorXZ(direction, rotSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + direction * 5);
    }
}
