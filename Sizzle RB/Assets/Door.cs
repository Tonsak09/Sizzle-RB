using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen;
    public float speed;
    public float distance;

    private float lerp;

    private Vector3 origin;
    private Vector3 target;

    private void Start()
    {
        origin = this.transform.position;
        target = origin + Vector3.up * distance;
    }

    private void Update()
    {
        if(IsOpen)
        {
            if(lerp > 1)
            {
                lerp = 1;
            }
            else
            {
                lerp += Time.deltaTime * speed;
            }
        }
        else
        {
            if (lerp < 0)
            {
                lerp = 0;
            }
            else
            {
                lerp -= Time.deltaTime * speed;
            }
        }

        this.transform.position = Vector3.Lerp(origin, target, lerp);

    }
}
