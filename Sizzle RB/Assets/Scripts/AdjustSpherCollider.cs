using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpherCollider : MonoBehaviour
{
    public float distanceDown;
    private SphereCollider sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = this.transform.position - Vector3.down * distanceDown + sc.radius * Vector3.up;
        sc.center = newPos;

    }
}
