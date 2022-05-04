using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegment : MonoBehaviour
{


    public Transform front;
    public Transform back;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = (front.position + back.position) / 2;
        this.transform.LookAt(front);
    }
}
