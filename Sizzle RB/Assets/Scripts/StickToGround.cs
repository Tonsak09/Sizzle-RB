using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToGround : MonoBehaviour
{

    public LayerMask mask;

    public float minRange;
    public float maxRange;

    public float floorOffset;

    private void Update()
    {
        Stick();
    }

    private void Stick()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, maxRange))
        {
            if (Vector3.Distance(hit.point, this.transform.position) > minRange)
            {
                print("stick");
                this.transform.position = hit.point + Vector3.up * floorOffset;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.down * maxRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.down * floorOffset);
    }
}
