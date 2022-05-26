using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public float detectRange;
    public Vector3 overlapBoxHalfExtents;

    public LayerMask heatMask;
    public LayerMask terrainMask;
    private Vector3 center { get { return this.transform.position - (this.transform.localScale / 2); } }


    private float lerp;
    private Vector3 origin;
    private Vector3 target;

    public float distance;
    public float speed;

    private enum SlimeStates { idle, moving}

    [SerializeField]
    private SlimeStates state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SlimeStates.idle:
                IdleState();
                break;
            case SlimeStates.moving:
                MovingState();

                break;
        }
        
    }

    private void IdleState()
    {
        Vector3 xPos = center + new Vector3(this.transform.localScale.x + detectRange, this.transform.localScale.y / 2, this.transform.localScale.z / 2);
        Vector3 xNeg = center + new Vector3(-(detectRange), this.transform.localScale.y / 2, this.transform.localScale.z / 2);
        CheckDirection(xPos, xNeg);
        CheckDirection(xNeg, xPos);

        Vector3 zPos = center + new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, this.transform.localScale.z + detectRange);
        Vector3 zNeg = center + new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, -detectRange);
        CheckDirection(zPos, zNeg);
        CheckDirection(zNeg, zPos);
    }

    private void CheckDirection(Vector3 CheckDirection, Vector3 opposite)
    {
        Collider[] collisions = Physics.OverlapBox(CheckDirection, overlapBoxHalfExtents, Quaternion.identity, heatMask);
        if (collisions.Length > 0)
        {
           for(int i = 0; i < collisions.Length; i++)
            {
                if(collisions[i].GetComponent<DestroyTimer>())
                {
                    Destroy(collisions[i]);
                }
            }
            Collider[] collisionOpposite = Physics.OverlapBox(opposite, overlapBoxHalfExtents, Quaternion.identity, terrainMask);
            print(collisionOpposite.Length);
            if (collisionOpposite.Length == 0)
            {
                state = SlimeStates.moving;
                target = this.transform.position + (Vector3.ProjectOnPlane(this.transform.position - CheckDirection, Vector3.up).normalized * distance);
                origin = this.transform.position;
            }
            else
            {
                // Show player that slime cannot move 
            }
        }
    }

    private void MovingState()
    {

        if (lerp <= 1)
        {
            // Moves smoothly to new point 
            this.transform.position = Vector3.Lerp(origin, target, lerp);

            lerp += Time.deltaTime * speed;
        }
        else
        {
            state = SlimeStates.idle;
            lerp = 0;
        }
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(center + new Vector3(this.transform.localScale.x + detectRange, this.transform.localScale.y / 2, this.transform.localScale.z / 2), overlapBoxHalfExtents * 2);
        Gizmos.DrawWireCube(center + new Vector3(-(detectRange), this.transform.localScale.y / 2, this.transform.localScale.z / 2), overlapBoxHalfExtents * 2);

        Gizmos.DrawWireCube(center + new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, this.transform.localScale.z + detectRange), overlapBoxHalfExtents * 2);
        Gizmos.DrawWireCube(center + new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, -detectRange), overlapBoxHalfExtents * 2);
        */
    }
}
