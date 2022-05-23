using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public Door door;
    public Vector3 offset;
    public float radius;

    public LayerMask mask;

    private void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(this.transform.position + offset, radius, mask);

        if(collisions.Length == 0)
        {
            door.IsOpen = false;
        }
        else
        {
            foreach (Collider collider in collisions)
            {
                // Checks if collider is above pressureplate 
                if (this.transform.position.y < collider.transform.position.y)
                {
                    door.IsOpen = true;
                    break;
                }

                // Can only be reached if all colliders are below 
                door.IsOpen = false;
            }
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        door.IsOpen = true;
    }

    private void OnTriggerStay(Collider other)
    {
        door.IsOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        door.IsOpen = false;
    }
    */

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position + offset, radius);
    }

}
