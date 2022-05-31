using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsController : MonoBehaviour
{
    [SerializeField] LegIKSolver frontLeft;
    [SerializeField] LegIKSolver frontRight;
    [SerializeField] LegIKSolver backLeft;
    [SerializeField] LegIKSolver backRight;

    [Range(0, 1)]
    [SerializeField] float lerpToMoveOpposite;

    private LegIKSolver[] frontPair;
    private bool frontCurrent;

    private LegIKSolver[] backPair;
    private bool backCurrent;

    // Start is called before the first frame update
    void Start()
    {
        frontPair = new LegIKSolver[] { frontLeft, frontRight };
        frontPair = new LegIKSolver[] { backLeft, backRight };

        frontLeft.TryMove();

        //StartCoroutine(LegUpdateCoroutine());
    }

    private void Update()
    {
        //RunPair(frontPair, frontCurrent);
        
        frontLeft.TryMove();
        backRight.TryMove();
        backLeft.TryMove();
        frontRight.TryMove();
        
    }

    private bool RunPair(LegIKSolver[] pair, bool current)
    {
        // Whether first or second in pair based on bool 
        int i = current ? 1 : 0;
        print(i);

        // If primary is over a certain lerp secondary can begin 
        if (pair[i].Lerp > lerpToMoveOpposite)
        {
            pair[(i + 1) % 2].TryMove();
        }

        return !pair[i].Moving;

    }

    public IEnumerator LegUpdateCoroutine()
    {
        while (true)
        {
            do
            {
                frontLeft.TryMove();
                backRight.TryMove();

                yield return null;
            } while (frontLeft.Moving || backRight.Moving);

            do
            {
                backLeft.TryMove();
                frontRight.TryMove();

                yield return null;
            } while (backLeft.Moving || frontRight.Moving);
        }
    }
}
