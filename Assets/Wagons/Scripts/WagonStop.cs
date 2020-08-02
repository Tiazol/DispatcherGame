using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class WagonStop : MonoBehaviour
{
    public static WagonStop Instance { get; private set; }

    private Transform[] checkPoints;
    private Wagon[] goals;

    private void Awake()
    {
        Instance = this;
        checkPoints = GetComponentsInChildren<Transform>();
        goals = GetComponentsInChildren<Wagon>();
    }

    public bool CheckPosition(Wagon wagon)
    {
        var check = wagon.transform.position.y >= transform.position.y;

        return check;

        //if (wagon.transform.position.y == transform.position.y)
        //{
        //    foreach (var point in checkPoints)
        //    {
        //        if (wagon.transform.position.x == point.position.x)
        //        {

        //        }
        //    }

        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }
}
