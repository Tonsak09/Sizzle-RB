using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public float range;

    private Collider collider;
    private Rigidbody rb;
    public float distance { get; set; }

    private Transform target;

    private void Start()
    {
        collider = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if(target != null)
        {
            this.transform.position = target.position;
        }
    }

    public IEnumerator GrabCandle(float speed, Transform target)
    {
        float lerp = 0;
        Vector3 origin = this.transform.position;

        // Makes sure that candle collider doesn't smack Sizzle in the face 
        collider.enabled = false;
        rb.useGravity = false;

        while(lerp < 1)
        {
            this.transform.position = Vector3.Lerp(origin, target.position, lerp);
            lerp += Time.deltaTime * speed;

            yield return null;
        }
        this.target = target;

    }

    public void SetCandle(float throwSpeed, Vector3 throwDirection)
    {
        target = null;

        collider.enabled = true;
        rb.useGravity = true;

        rb.AddForce(throwDirection * throwSpeed, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
