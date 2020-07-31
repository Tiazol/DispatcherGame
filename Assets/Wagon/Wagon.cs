using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(0, 1);
        transform.Translate(direction * speed);
    }
}
