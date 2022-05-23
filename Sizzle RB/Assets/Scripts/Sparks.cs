using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{

    public Transform sparksStart;
    public GameObject sparksFX;

    public GameObject triggerSphere;

    public float coolDown;
    public float triggerTime;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On spark click 
        if(Input.GetMouseButton(0))
        {
            // If cooldown is over 
            if(timer >= coolDown)
            {
                // Creates fx 
                Instantiate(sparksFX, sparksStart.position, this.transform.rotation);

                // Creates temporary trigger box for slime detection 
                GameObject temp = Instantiate(triggerSphere, triggerSphere.transform.position, Quaternion.identity);
                temp.GetComponent<Collider>().enabled = true;

                DestroyTimer dt = temp.AddComponent<DestroyTimer>();
                dt.time = 1;

                // Rests timer 
                timer = 0;
            }
        }

        timer += Time.deltaTime;
    }
}
