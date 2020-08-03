using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class WagonStop : MonoBehaviour
{
    public static WagonStop Instance { get; private set; }
    public List<WagonType> UsedWagonTypes { get; set; }

    private void Awake()
    {
        Instance = this;
        UsedWagonTypes = new List<WagonType>();
    }
}
