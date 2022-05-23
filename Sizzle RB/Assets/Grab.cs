using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public KeyCode grabKey;
    public float range;

    public float grabSpeed;
    public float throwSpeed;
    public Vector3 throwOffset;

    public Transform grabPoint;

    private Candle[] candles;
    private Candle heldCandle;


    // Start is called before the first frame update
    void Start()
    {
        candles = FindObjectsOfType<Candle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if(heldCandle == null)
            {
                GrabCandle();
            }
            else
            {
                heldCandle.SetCandle(throwSpeed, (grabPoint.forward + throwOffset).normalized);
                heldCandle = null;
            }
        }
    }

    private void GrabCandle()
    {
        List<Candle> candlesInRange = new List<Candle>();

        // Get all candles within range 
        foreach (Candle candle in candles)
        {
            if( (candle.distance = Vector3.Distance(candle.transform.position, grabPoint.position)) < range)
            {
                candlesInRange.Add(candle);
            }
        }

        // Makes sure there is actually a candles within range 
        if(candlesInRange.Count > 0)
        {
            // Find closest candle 
            Candle closest = null;
            foreach (Candle item in candlesInRange)
            {
                if (closest == null || item.distance < closest.distance)
                {
                    closest = item;
                }
            }

            heldCandle = closest;

            // At least one candle 
            StartCoroutine(closest.GrabCandle(grabSpeed, grabPoint));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(grabPoint.position, grabPoint.position + (grabPoint.forward + throwOffset).normalized * 4);
    }

}
