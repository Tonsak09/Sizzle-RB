using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Movement mainJoint;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        // Reverses transform so lookat function works properly 
        this.transform.localScale += this.transform.localScale.z * -Vector3.forward * 2;

        offset = Vector3.Distance(this.transform.position, mainJoint.transform.position);
    }

    private void LateUpdate()
    {
        // Keeps the head in front of the main joint
        this.transform.position = mainJoint.transform.position + mainJoint.Direction * offset;
        this.transform.LookAt(mainJoint.transform);
    }
}
