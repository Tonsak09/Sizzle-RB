using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{

    public Rigidbody[] bodySegments;
    public float followSpeed;

    private float[] chainDistances;


    public float moveSpeed;
    public float rotateSpeed;

    private Rigidbody rb;




    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        chainDistances = new float[bodySegments.Length];

        for (int i = 1; i < bodySegments.Length; i++)
        {
            chainDistances[i] = Vector3.Distance(bodySegments[i].transform.position, bodySegments[i - 1].transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        HeadMove();
        UpdateSegments();

    }


    /// <summary>
    /// Checks if any part of the body chain needs to be updated
    /// with its position 
    /// </summary>
    private void UpdateSegments()
    {
        for (int i = 1; i < bodySegments.Length; i++)
        {
            bodySegments[i].transform.LookAt(bodySegments[i - 1].transform.position, Vector3.up);

            // Direction from current to parent 
            Vector3 dir = bodySegments[i - 1].transform.position - bodySegments[i].transform.position;


            // Gets target starting from the parents position 
            Vector3 target = bodySegments[i - 1].transform.position - dir.normalized * chainDistances[i];

            // If past the target 
            if(dir.magnitude < target.magnitude)
            {
                bodySegments[i].transform.position = target;
            }
            else if (!EqualWithinRange(dir.magnitude, chainDistances[i], 0.1f))
            {
                bodySegments[i].velocity = (target - bodySegments[i].transform.position).normalized * followSpeed * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Moves the head around using rigidbody velocities 
    /// </summary>
    private void HeadMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RaycastHit hit;
            Physics.Raycast(this.transform.position, Vector3.down, out hit);

            rb.velocity = rb.velocity + moveSpeed * Vector3.ProjectOnPlane(this.transform.forward, hit.normal).normalized * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0));
        }
    }

    /// <summary>
    /// Checks if a given value is equal to a check within a certain tolerance 
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    private bool EqualWithinRange(float A, float B, float tolerance)
    {
        return Mathf.Abs(A - B) <= tolerance;
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit);

        Vector3 forward = Vector3.ProjectOnPlane(this.transform.forward, hit.normal);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(this.transform.position, this.transform.position + forward * 5);

    }
}
