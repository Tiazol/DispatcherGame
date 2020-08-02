using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonStop : MonoBehaviour
{
    public static WagonStop Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool CheckPosition(float coordY)
    {
        var check = coordY >= transform.position.y;

        return check;
    }
}
