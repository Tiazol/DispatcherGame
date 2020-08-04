using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStockContainer : MonoBehaviour
{
    public static RollingStockContainer Instance { get; private set; }

    public CheckpointsManager firstStopper;
    public CheckpointsManager secondStopper;
    public CheckpointsManager lastStopper;

    public bool CheckPosition(float coordY)
    {
        var check = coordY == firstStopper.transform.position.y;
        if (check)
        {
            //firstStopper.HasWagon = true;
        }
        return check;
    }

    //public bool IsNextStopperIsFree(Stopper stopper)
    //{

    //}
}
