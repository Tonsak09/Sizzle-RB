using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinFromGround : MonoBehaviour
{
    public LayerMask mask;
    public float minDistance;

    // Update is called once per frame
    private void LateUpdate()
    {
        // Perhaps change to normal of object hit? 
        
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.down, out hit, minDistance, mask))
        {
            this.transform.position = hit.point + Vector3.up * minDistance;
        }
    }
}
